using CleanTodo.Core.Application.Commands.TodoItems;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{
    public class SetTodoItemCompletionStatusShould : TestingBase
    {
        [Fact]
        public async Task GivenTodoItemMarkedComplete_WhenHandled_WillBeCompleted()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item."
            };

            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));
            _dbContext.ChangeTracker.Clear();

            var updateResponse = await _mediator.Send(new SetTodoItemCompletionStatusCommand(createResponse.Id, true));

            updateResponse.IsComplete.Should().BeTrue();
        }

        [Fact]
        public async Task GivenTodoItemMarkedIncomplete_WhenHandled_WillBeIncomplete()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item."
            };

            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));
            _dbContext.ChangeTracker.Clear();

            var updateResponse = await _mediator.Send(new SetTodoItemCompletionStatusCommand(createResponse.Id, false));

            updateResponse.IsComplete.Should().BeFalse();
        }



        [Fact]
        public async Task GivenTodoItemMarkedComplete_WhenHandled_WillBeCompleteWithTodaysDate()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item."
            };

            var createResponse = await _mediator.Send(new CreateTodoItemCommand(createRequest));
            _dbContext.ChangeTracker.Clear();

            var updateResponse = await _mediator.Send(new SetTodoItemCompletionStatusCommand(createResponse.Id, true));

            updateResponse.CompletionDate.Should().Be(DateTime.Today);
        }
    }
}
