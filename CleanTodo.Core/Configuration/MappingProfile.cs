using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Mapping;
using System.Reflection;

namespace CleanTodo.Core.Configuration
{ 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AddMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void AddMappingsFromAssembly(Assembly assembly)
        {
            // Get all types in the assembly that are implementing IMapTo<> interface
            var mappableTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(IsMappableType))
                .ToList();

            foreach (var mappableType in mappableTypes)
            {
                // Create an instance of the type and see if the type implemented 
                // the ConfigureMapping method. If so, invoke it and continue on.
                var instance = Activator.CreateInstance(mappableType);
                var methodName = nameof(IMapTo<object>.ConfigureMapping);
                var method = mappableType.GetMethod(methodName);

                if (method != null)
                {
                    method.Invoke(instance, new object[] { this });
                    continue;
                }

                // If the current type makes it here, then the implementation for
                // ConfigureMapping is implemented on the interface itself as part
                // of it's default implementation and needs to be invoked there.
                var interfaceTypes = mappableType.GetInterfaces().Where(IsMappableType);
                if (interfaceTypes.Any())
                {
                    interfaceTypes.ToList().ForEach(interfaceType =>
                    {
                        interfaceType.GetMethod(methodName, new Type[] { typeof(Profile) })?.Invoke(instance, new object[] { this });
                    });
                }
            }
        }

        private bool IsMappableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IMapTo<>);
    }
}
