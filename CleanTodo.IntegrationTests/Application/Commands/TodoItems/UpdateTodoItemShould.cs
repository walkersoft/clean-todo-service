using CleanTodo.Core.Application.Commands.TodoItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public class UpdateTodoItemShould : TestingBase
    {
        public UpdateTodoItemShould() : base() { }

        [Fact]
        public async Task GivenExistingTodoItemIsEdited_WhenHandled_ItemIsUpdated()
        {
            var createRequest = new TodoItemRequest() { Description = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));

            _dbContext.ChangeTracker.Clear();
        }
    }
}
