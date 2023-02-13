using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoTags;
using FluentAssertions;
using System;
using System.Linq;
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
            response.Description.Should().Be(request.Description);
        }

        [Fact]
        public async Task GivenTodoItemWithTagIds_WhenHandled_WillPopulateTheTags()
        {
            var firstTagRequest = new CreateTodoTagRequest() { Name = "Foo" };
            var secondTagRequest = new CreateTodoTagRequest() { Name = "Bar" };
            var firstTagResponse = await _mediator.Send(new CreateTodoTagCommand(firstTagRequest));
            var secondTagResponse = await _mediator.Send(new CreateTodoTagCommand(secondTagRequest));

            var createTodoRequest = new CreateTodoItemRequest { Description = "Todo item..." };
            createTodoRequest.TagIds.Add(firstTagResponse.Id);
            createTodoRequest.TagIds.Add(secondTagResponse.Id);

            var createTodoResponse = await _mediator.Send(new CreateTodoItemCommand(createTodoRequest));

            createTodoResponse.Tags.Should().NotBeEmpty();
            createTodoResponse.Tags.Should().Contain(firstTagResponse.Id);
            createTodoResponse.Tags.Should().Contain(secondTagResponse.Id);
        }
    }
}
