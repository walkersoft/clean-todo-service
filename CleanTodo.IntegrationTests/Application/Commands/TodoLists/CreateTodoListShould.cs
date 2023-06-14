using CleanTodo.Core.Application.Commands.TodoLists;
using FluentAssertions;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoLists
{
    public sealed class CreateTodoListShould : TestingBase
    {
        [Fact]
        public async Task GivenValidTodoList_WhenHandled_WillSucceed()
        {
            var request = new TodoListRequest()
            {
                Title = "Title",
                Description = "Description",
            };

            var response = await _mediator.Send(new CreateTodoListCommand(request));

            response.Id.Should().NotBe(Guid.Empty);
        }
    }
}
