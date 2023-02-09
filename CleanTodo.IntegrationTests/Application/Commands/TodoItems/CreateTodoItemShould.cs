using CleanTodo.Core.Application.Commands.TodoItems;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public sealed class CreateTodoItemShould
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;

        public CreateTodoItemShould()
        {
            _factory = new TestWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _mediator = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task GivenValidTodoItemRequest_WhenHandled_WillSucceed()
        {
            var request = new CreateTodoItemRequest()
            {
                Description = "This is a todo item.",
            };

            var response = await _mediator.Send(new CreateTodoItemCommand(request));

            response.Id.Should().NotBe(default(Guid));
        }
    }
}
