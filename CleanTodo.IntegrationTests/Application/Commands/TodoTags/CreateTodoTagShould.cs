using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanTodo.IntegrationTests.Application.Commands.TodoTags
{
    public sealed class CreateTodoTagShould : TestingBase
    {
        public CreateTodoTagShould() : base() { }

        [Fact]
        public async Task GivenValidTodoTagRequest_WhenHandled_WillSucceed()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };

            var response = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            response.Id.Should().NotBe(default(Guid));
            response.Name.Should().Be(createRequest.Name);
        }

        [Fact]
        public async Task GivenDuplicateNameWithDifferentCase_WhenHandled_WillThrowException()
        {
            var firstTagRequest = new TodoTagRequest() { Name = "Foo" };
            var secondTagRequest = new TodoTagRequest() { Name = "foo" };

            await _mediator.Send(new CreateTodoTagCommand(firstTagRequest));
            var action = async () => await _mediator.Send(new CreateTodoTagCommand(secondTagRequest));

            await action.Should().ThrowAsync<DuplicateTagException>();
        }

        [Fact]
        public async Task GivenDuplicateNameWithLeadingOrTrailingWhitespace_WhenHandled_WillThrowException()
        {
            var firstTagRequest = new TodoTagRequest() { Name = "Foo" };
            var secondTagRequest = new TodoTagRequest() { Name = " Foo " };

            await _mediator.Send(new CreateTodoTagCommand(firstTagRequest));
            var action = async () => await _mediator.Send(new CreateTodoTagCommand(secondTagRequest));

            await action.Should().ThrowAsync<DuplicateTagException>();
        }

        [Fact]
        public async Task GivenTodoTagWithNoName_WhenHandled_WillThrowException()
        {
            var tagRequest = new TodoTagRequest() { Name = "" };

            var action = async () => await _mediator.Send(new CreateTodoTagCommand(tagRequest));

            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
