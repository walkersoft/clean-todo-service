﻿using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using CleanTodo.Infrastructure.Data.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public Task<TEntity> FirstOrNotFound<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return Set<TEntity>().FirstOrNotFound(id);
        }

        public async Task<Guid> GetExistingTagId(string tagName)
        {
            var tag = await TodoTags.SingleOrDefaultAsync(
                tag => tagName.ToLower().Trim() == tag.Name.ToLower().Trim()
            );

            return tag?.Id ?? Guid.Empty;
        }
    }
}
