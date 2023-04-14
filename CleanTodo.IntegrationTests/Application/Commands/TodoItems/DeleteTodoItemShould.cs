using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public class DeleteTodoItemShould : TestingBase
    {
        public DeleteTodoItemShould() : base() { }

        [Fact]
        public async Task GivenTodoItemThatExists_WhenHandled_WillDeleteTodoItem()
        {
            var createRequest = new TodoItemRequest() { Description = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));
            _dbContext.ChangeTracker.Clear();

            var deleteAction = async () => await _mediator.Send(new DeleteTodoItemCommand(createResponse.Id));

            await deleteAction.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenTodoItemThatDoesNotExist_WhenHandled_WillThrowException()
        {
            var deleteAction = async () => await _mediator.Send(new DeleteTodoItemCommand(Guid.NewGuid()));
            await deleteAction.Should().ThrowAsync<EntityNotFoundException>();
        }
    }
}
