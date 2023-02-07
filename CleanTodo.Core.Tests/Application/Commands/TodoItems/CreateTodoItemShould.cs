using CleanTodo.Core.Application.Commands.TodoItems;
using FluentAssertions;
using Xunit;

namespace CleanTodo.Core.Tests.Application.Commands.TodoItems
{    
    public sealed class CreateTodoItemShould
    {
        [Fact]
        public void GivenValidTodoItemRequest_WhenHandled_WillSucceed()
        {
            var request = new CreateTodoItemRequest()
            {
                Description = "This is a todo item.",
            };

            var command = new CreateTodoItemCommand(request);
            var handler = new CreateTodoItemHandler();
            var response = handler.Handle(command, new CancellationToken()).Result;

            response.Id.Should().NotBe(default(Guid));
        }
    }
}
