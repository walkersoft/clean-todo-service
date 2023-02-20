using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Infrastructure.Data.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<TEntity> FirstOrNotFound<TEntity>(this DbSet<TEntity> dbSet, Guid id) where TEntity : BaseEntity
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(string.Format("Unable to locate entity of type: {0} with ID: {1} in the database.", dbSet.EntityType.Name, id));
            }

            return entity;
        }
    }
}
