using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoLists;
using CleanTodo.Core.Application.Queries.TodoItems;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public class AssignTodoItemToListShould : TestingBase
    {
        [Fact]
        public async Task GivenTodoItemAssignedToExistingList_WhenHandled_WillSucceed()
        {
            var createTodoItemRequest = new TodoItemRequest()
            {
                Description = "Todo Item"
            };

            var createTodoListRequest = new TodoListRequest()
            {
                Title = "Todo List"
            };

            var createTodoItemResponse = await _mediator.Send(new CreateTodoItemCommand(createTodoItemRequest));
            var createListResponse = await _mediator.Send(new CreateTodoListCommand(createTodoListRequest));
            _dbContext.ChangeTracker.Clear();

            var assignTodoItemRequest = new AssignTodoItemRequest()
            {
                TodoItemId = createTodoItemResponse.Id,
                TodoListId = createListResponse.Id
            };

            await _mediator.Send(new AssignTodoItemCommand(assignTodoItemRequest));
            _dbContext.ChangeTracker.Clear();

            var getListResponse = await _mediator.Send(new GetAllTodoItemsQuery());

            getListResponse.Any(todoItem => todoItem.Id == createTodoItemResponse.Id)
                .Should().BeTrue();
        }
    }
}
