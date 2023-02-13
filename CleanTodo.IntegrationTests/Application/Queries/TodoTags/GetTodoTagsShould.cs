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
            var request = new CreateTodoTagRequest() { Name = "Foo" };

            await _mediator.Send(new CreateTodoTagCommand(request));
            var response = await _mediator.Send(new GetAllTodoTagsQuery());

            response.Any().Should().BeTrue();
        }
    }
}
