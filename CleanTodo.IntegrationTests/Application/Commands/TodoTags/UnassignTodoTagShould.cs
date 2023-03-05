using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Application.Queries.TodoTags;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoTags
{
    public class UnassignTodoTagShould : TestingBase
    {
        public UnassignTodoTagShould() { }

        [Fact]
        public async Task GivenExistingTagToUnassign_WhenHandled_WillRemoveTagFromAllItems()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var itemRequest = new CreateTodoItemRequest() { Description = "Bar" };
            itemRequest.TagIds.Add(createResponse.Id);
            await _mediator.Send(new CreateTodoItemCommand(itemRequest));
            var tagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            tagsResponse.First().AssignedCount.Should().Be(1);
            _dbContext.ChangeTracker.Clear();
            var unassignResponse = await _mediator.Send(new UnassignTodoTagCommand(new TodoTagRequest() { Id = tagsResponse.First().Id }));

            unassignResponse.IsAssigned.Should().BeFalse();
            unassignResponse.AssignedCount.Should().Be(0);
        }
    }
}
