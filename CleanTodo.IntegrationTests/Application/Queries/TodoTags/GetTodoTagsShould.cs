﻿using CleanTodo.Core.Application.Commands.TodoItems;
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
            
            _dbContext.ChangeTracker.Clear();
            var response = await _mediator.Send(new GetAllTodoTagsQuery());

            response.Any().Should().BeTrue();
        }

        [Fact] 
        public async Task FetchTodoTagsBelongingToItems_WhenHandled_WillShowTagsAreAssigned()
        {
            var tagResponse = await _mediator.Send(
                new CreateTodoTagCommand(new TodoTagRequest() { Name = "Foo" })
            );

            var itemRequest = new TodoItemRequest() { Description = "Bar" };
            itemRequest.TagIds.Add(tagResponse.Id);
            await _mediator.Send(new CreateTodoItemCommand(itemRequest));
            
            _dbContext.ChangeTracker.Clear();
            var tagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            tagsResponse.All(x => x.IsAssigned == true).Should().BeTrue();
        }

        [Fact]
        public async Task FetchTodoTagsNotBelongingToItems_WhenHandled_WillShowTagsAreUnassigned()
        {
            await _mediator.Send(new CreateTodoTagCommand(new TodoTagRequest() { Name = "Foo" }));
            await _mediator.Send(new CreateTodoTagCommand(new TodoTagRequest() { Name = "Bar" }));
            await _mediator.Send(new CreateTodoTagCommand(new TodoTagRequest() { Name = "Baz" }));

            _dbContext.ChangeTracker.Clear();
            var tagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            tagsResponse.Any().Should().BeTrue();
            tagsResponse.All(x => x.IsAssigned == false).Should().BeTrue();
        }

        [Fact]
        public async Task FetchTodoTags_WhenHandled_WillShowAssignedTodoCounts()
        {
            var tagResponse = await _mediator.Send(
                new CreateTodoTagCommand(new TodoTagRequest() { Name = "Foo" })
            );

            var itemRequest = new TodoItemRequest() { Description = "Bar" };
            itemRequest.TagIds.Add(tagResponse.Id);
            await _mediator.Send(new CreateTodoItemCommand(itemRequest));

            _dbContext.ChangeTracker.Clear();

            var tagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            tagsResponse.First().AssignedCount.Should().Be(1);
        }
    }
}
