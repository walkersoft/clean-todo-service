using CleanTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Interfaces
{
    public interface ITodoApplicationDbContext
    {
        Task SaveChangesAsync();
        void Add<T>(T entity) where T : BaseEntity;
    }
}
