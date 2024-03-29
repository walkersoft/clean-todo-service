﻿using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Queries.TodoItems;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Queries.TodoItems
{
    public class GetTodoItemsShould : TestingBase
    {
        public GetTodoItemsShould() : base() { }

        [Fact]
        public async Task FetchTodoItems_WillSucceed()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item.",
            };

            await _mediator.Send(new CreateTodoItemCommand(createRequest));

            _dbContext.ChangeTracker.Clear();
            var response = await _mediator.Send(new GetAllTodoItemsQuery());

            response.Any().Should().BeTrue();
        }

        [Fact]
        public async Task FetchTodoItems_WithItemsThatRollOver_WillCalculateRollOverQuantity()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item.",
                DueDate = DateTime.Now.AddDays(-3),
                RollsOver = true
            };

            await _mediator.Send(new CreateTodoItemCommand(createRequest));

            _dbContext.ChangeTracker.Clear();
            var response = await _mediator.Send(new GetAllTodoItemsQuery());

            response.First().RollOverCount.Should().Be(3);
        }

        [Fact]
        public async Task FetchTodoItems_WithItemsThatDoNotRollOver_WillHaveZeroRollOverQuantity()
        {
            var createRequest = new TodoItemRequest()
            {
                Description = "This is a todo item.",
                DueDate = DateTime.Now.AddDays(-3),
                RollsOver = false
            };

            await _mediator.Send(new CreateTodoItemCommand(createRequest));

            _dbContext.ChangeTracker.Clear();
            var response = await _mediator.Send(new GetAllTodoItemsQuery());

            response.First().RollOverCount.Should().Be(0);
        }
    }
}
