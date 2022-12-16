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
    internal class TodoItemConfig : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.DueDate).HasDefaultValue(DateTime.Now);

            builder.HasMany(x => x.Tags)
                .WithMany(x => x.TodoItems)
                .UsingEntity(e => e.ToTable("TodoItemTags"));
        }
    }
}
