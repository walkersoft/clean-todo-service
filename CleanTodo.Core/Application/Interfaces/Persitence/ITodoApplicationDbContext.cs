using CleanTodo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Interfaces.Persitence
{
    public interface ITodoApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; }
        DbSet<TodoList> TodoLists { get; }
        DbSet<TodoTag> TodoTags { get; }
        Task<int> SaveChangesAsync();
        Task<TEntity> FirstOrNotFound<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
