using AutoMapper;

namespace CleanTodo.Core.Application.Interfaces.Mapping
{
    public interface IMapTo<TDestination>
    {
        void ConfigureMapping(IProfileExpression config) => config.CreateMap(GetType(), typeof(TDestination));
    }
}
