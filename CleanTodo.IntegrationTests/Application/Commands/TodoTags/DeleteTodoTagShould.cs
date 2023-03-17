using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoTags
{
    public class DeleteTodoTagShould : TestingBase
    {
        public DeleteTodoTagShould() { }

        [Fact]
        public async Task GivenTodoTagThatExists_WhenHandled_WillSucceed()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var deleteAction = async () => await _mediator.Send(new DeleteTodoTagCommand(createResponse.Id));

            await deleteAction.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenTodoTagThatWasDeletedAndIsDeletedAgain_WhenHandled_WillThrowException()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var deleteAction = async () => await _mediator.Send(new DeleteTodoTagCommand(createResponse.Id));

            await deleteAction.Should().NotThrowAsync();
            await deleteAction.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GivenTodoTagThatDoesNotExists_WhenHandled_WillThrowException()
        {
            var deleteAction = async () => await _mediator.Send(new DeleteTodoTagCommand(Guid.NewGuid()));
            await deleteAction.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GivenTodoTagThatIsAssigned_WhenHandled_WillThrowException()
        {
            var createTagRequest = new TodoTagRequest() { Name = "Foo" };
            var createTagResponse = await _mediator.Send(new CreateTodoTagCommand(createTagRequest));

            var createItemRequest = new TodoItemRequest() { Description = "Bar" };
            createItemRequest.TagIds.Add(createTagResponse.Id);
            await _mediator.Send(new CreateTodoItemCommand(createItemRequest));

            var deleteAction = async () => await _mediator.Send(new DeleteTodoTagCommand(createTagResponse.Id));

            await deleteAction.Should().ThrowAsync<AssignedTagRemovalException>();
        }
    }
}
