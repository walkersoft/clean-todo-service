using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoTags;
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
    public class UpdateTodoItemShould : TestingBase
    {
        public UpdateTodoItemShould() : base() { }

        [Fact]
        public async Task GivenExistingTodoItemWithDescriptionEdited_WhenHandled_ItemIsUpdated()
        {
            var createRequest = new TodoItemRequest() { Description = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));

            _dbContext.ChangeTracker.Clear();

            var updateRequest = new TodoItemRequest()
            {
                Id = createResponse.Id,
                Description = "Bar"
            };
            var updateResponse = await _mediator.Send(new UpdateTodoItemCommand(updateRequest));

            updateResponse.Id.Should().Be(createResponse.Id);
            updateResponse.Description.Should().Be(updateRequest.Description);
        }

        [Fact]
        public async Task GivenExistingTodoItemThatChangedCompletely_WhenHandled_ItemIsUpdated()
        {
            var createFooTagResponse = await _mediator.Send(new CreateTodoTagCommand(new TodoTagRequest() { Name = "Foo" }));
            var createBarTagResponse = await _mediator.Send(new CreateTodoTagCommand(new TodoTagRequest() { Name = "Bar" }));

            var createTodoItemRequest = new TodoItemRequest()
            {
                Description = "Foo",
                IsActive = true,
                DueDate = DateTime.Today,
                RollsOver = true
            };
            createTodoItemRequest.TagIds.Add(createFooTagResponse.Id);

            var createTodoItemResponse = await _mediator.Send(new CreateTodoItemCommand(createTodoItemRequest));
            _dbContext.ChangeTracker.Clear();

            var updateTodoItemRequest = new TodoItemRequest()
            {
                Id = createTodoItemResponse.Id,
                Description = "Bar",
                IsActive = false,
                DueDate = DateTime.Today.AddDays(1),
                RollsOver = false
            };
            updateTodoItemRequest.TagIds.Add(createFooTagResponse.Id);
            updateTodoItemRequest.TagIds.Add(createBarTagResponse.Id);

            await _mediator.Send(new UpdateTodoItemCommand(updateTodoItemRequest));
            _dbContext.ChangeTracker.Clear();

            var todoItems = await _mediator.Send(new GetAllTodoItemsQuery());
            var todoItem = todoItems.Where(x => x.Id == updateTodoItemRequest.Id).First();

            todoItem.Id.Should().Be(updateTodoItemRequest.Id);
            todoItem.Description.Should().Be(updateTodoItemRequest.Description);
            todoItem.IsActive.Should().BeFalse();
            todoItem.RollsOver.Should().BeFalse();
            todoItem.DueDate.Should().Be(updateTodoItemRequest.DueDate);
            todoItem.Tags.Count.Should().Be(2);
        }
    }
}
