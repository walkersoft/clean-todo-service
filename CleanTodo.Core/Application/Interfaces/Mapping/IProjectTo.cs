using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Interfaces.Mapping
{
    public interface IProjectTo<TSource, TDestination>
    {
        void ConfigureProjection(IProfileExpression profile) => profile.CreateProjection<TSource, TDestination>();
    }
}
