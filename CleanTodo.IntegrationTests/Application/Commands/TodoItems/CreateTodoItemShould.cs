using CleanTodo.Core.Application.Commands.TodoItems;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public sealed class CreateTodoItemShould : TestingBase
    {
        public CreateTodoItemShould() : base()
        {
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
