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
                    InvokeConfigurationMethod(interfaceTypes, methodName, instance);
                }
            }

            // Next, do the same thing, but look for projections being created
            // instead of maps
            var projectableTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(IsProjectableType))
                .ToList();

            foreach (var projectableType in projectableTypes)
            {
                // Create an instance of the type and see if the type implemented 
                // the ConfigureProjection method. If so, invoke it and continue on.
                var instance = Activator.CreateInstance(projectableType);
                var methodName = nameof(IProjectTo<object, object>.ConfigureProjection);
                var method = projectableType.GetMethod(methodName);

                if (method != null)
                {
                    method.Invoke(instance, new object[] { this });
                    continue;
                }

                // If the current type makes it here, then the implementation for
                // ConfigureProjection is implemented on the interface itself as part
                // of it's default implementation and needs to be invoked there.
                var interfaceTypes = projectableType.GetInterfaces().Where(IsProjectableType);
                if (interfaceTypes.Any())
                {
                    InvokeConfigurationMethod(interfaceTypes, methodName, instance);
                }
            }
        }

        private bool IsMappableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IMapTo<>);
        private bool IsProjectableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IProjectTo<,>);

        private void InvokeConfigurationMethod(IEnumerable<Type> interfaceTypes, string methodName, object? instance)
        {
            interfaceTypes.ToList().ForEach(interfaceType =>
            {
                interfaceType.GetMethod(methodName, new Type[] { typeof(Profile) })?.Invoke(instance, new object[] { this });
            });
        }
    }
}
