using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.IntegrationTests
{
    abstract public class TestingBase
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly IServiceScope _serviceScope;
        protected readonly IMediator _mediator;

        public TestingBase()
        {
            _factory = new TestWebApplicationFactory();            
            _serviceScope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();            
            _mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }
    }
}
