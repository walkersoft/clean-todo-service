using CleanTodo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Infrastructure.Data.Configuration
{
    internal class TodoListConfig : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.DueDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.ActivationDate).HasDefaultValueSql("GETDATE()");
            builder.HasMany(x => x.TodoItems);
            builder.Navigation(x => x.TodoItems).AutoInclude();
        }
    }
}
