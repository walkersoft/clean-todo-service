using AutoMapper;

namespace CleanTodo.Core.Application.Interfaces.Mapping
{
    public interface IMapTo<TDestination>
    {
        void ConfigureMapping(IProfileExpression profile) => profile.CreateMap(GetType(), typeof(TDestination));
    }
}
