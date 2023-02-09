using AutoMapper;
using CleanTodo.Api.Tests;
using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoItems
{    
    public sealed class CreateTodoItemShould
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly TodoDbContext _db;
        private readonly IServiceScopeFactory _scopeFactory;

        public CreateTodoItemShould()
        {
            _factory = new TestWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _db = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<TodoDbContext>();
        }

        [Fact]
        public void GivenValidTodoItemRequest_WhenHandled_WillSucceed()
        {
            using var scope = _scopeFactory.CreateScope();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            var request = new CreateTodoItemRequest()
            {
                Description = "This is a todo item.",
            };

            var command = new CreateTodoItemCommand(request);
            var handler = new CreateTodoItemHandler(_db, mapper);
            
            var response = handler.Handle(command, new CancellationToken()).Result;
            
            response.Id.Should().NotBe(default(Guid));
        }
    }
}
