using CleanTodo.Core.Application.Commands.TodoLists;
using CleanTodo.Core.Application.Queries.TodoLists;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Queries.TodoLists
{
    public sealed class GetTodoListsShould : TestingBase
    {
        [Fact]
        public async Task FetchTodoLists_WillSucceed()
        {
            var request = new TodoListRequest()
            {
                Title = "Title",
                Description = "Description"
            };

            await _mediator.Send(new CreateTodoListCommand(request));

            _dbContext.ChangeTracker.Clear();
            var response = await _mediator.Send(new GetAllTodoListsQuery());

            response.Any().Should().BeTrue();
        }
    }
}
