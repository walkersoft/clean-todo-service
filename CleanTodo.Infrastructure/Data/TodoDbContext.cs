using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using CleanTodo.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Infrastructure.Data
{
    public class TodoDbContext : DbContext, ITodoApplicationDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoTag> TodoTags { get; set; }

        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public Task<TEntity> FirstOrNotFound<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return Set<TEntity>().FirstOrNotFound(entity.Id);
        }
    }
}
