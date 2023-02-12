using CleanTodo.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.IntegrationTests
{
    abstract public class TestingBase : IDisposable
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly IServiceScope _serviceScope;
        private TodoDbContext _dbContext;
        protected readonly IMediator _mediator;
        

        public TestingBase()
        {
            _factory = new TestWebApplicationFactory();            
            _serviceScope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();            
            _mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
            _dbContext = _serviceScope.ServiceProvider.GetRequiredService<TodoDbContext>();
            InitalizeDatabase();
        }

        private void InitalizeDatabase()
        {
            _dbContext.Database.Migrate();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
            GC.SuppressFinalize(this); // See CA1816
        }
    }
}
