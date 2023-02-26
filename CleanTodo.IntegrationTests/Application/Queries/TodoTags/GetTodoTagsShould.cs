using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Application.Queries.TodoTags;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Queries.TodoTags
{
    public sealed class GetTodoTagsShould : TestingBase
    {
        public GetTodoTagsShould() : base() { }

        [Fact]
        public async Task FetchTodoTags_WillSucceed()
        {
            var request = new TodoTagRequest() { Name = "Foo" };

            await _mediator.Send(new CreateTodoTagCommand(request));
            var response = await _mediator.Send(new GetAllTodoTagsQuery());

            response.Any().Should().BeTrue();
        }

        [Fact] 
        public async Task FetchTodoTagsBelongingToItems_WhenHandled_WillShowTagsAreAssigned()
        {
            var tagResponse = await _mediator.Send(
                new CreateTodoTagCommand(new TodoTagRequest() { Name = "Foo" })
            );

            var itemRequest = new CreateTodoItemRequest() { Description = "Bar" };
            itemRequest.TagIds.Add(tagResponse.Id);
            await _mediator.Send(new CreateTodoItemCommand(itemRequest));

            var tagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            tagsResponse.All(x => x.IsAssigned).Should().BeTrue();
        }
    }
}
