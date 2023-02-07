using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Interfaces;
using FluentAssertions;
using MediatR;
using Moq;
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
            var handler = new Mock<IRequestHandler<CreateTodoItemCommand, TodoItemResponse>>();
            handler.Setup(x => x.Handle(command, new CancellationToken())).Returns(Task.FromResult(new TodoItemResponse() { Id = Guid.NewGuid() }));
            
            var response = handler.Object.Handle(command, new CancellationToken()).Result;
            
            response.Id.Should().NotBe(default(Guid));
        }
    }
}
