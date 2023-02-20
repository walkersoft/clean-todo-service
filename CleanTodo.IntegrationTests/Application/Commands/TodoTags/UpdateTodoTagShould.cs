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
    public sealed class UpdateTodoTagShould : TestingBase
    {
        public UpdateTodoTagShould() { }

        [Fact]
        public async Task GivenAnExistingTagWithNew_WhenHandled_WillSucceed()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var updateRequest = new TodoTagRequest() { Id = createResponse.Id, Name = "Bar" };
            var updateResponse = await _mediator.Send(new UpdateTodoTagCommand(updateRequest));

            updateResponse.Id.Should().Be(createResponse.Id);
            updateResponse.Name.Should().Be(updateRequest.Name);
        }

        [Fact]
        public async Task GivenTagIdThatDoesNotExist_WhenHandled_WillThrowEntityNotFoundException()
        {
            var request = new TodoTagRequest() { Id = Guid.NewGuid(), Name = "Foo" };
            var action = async () => await _mediator.Send(new UpdateTodoTagCommand(request));

            await action.Should().ThrowAsync<EntityNotFoundException>();
        }
    }
}
