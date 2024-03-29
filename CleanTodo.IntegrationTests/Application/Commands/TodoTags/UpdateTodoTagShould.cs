﻿using CleanTodo.Core.Application.Commands.TodoTags;
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
    public sealed class UpdateTodoTagShould : TestingBase
    {
        public UpdateTodoTagShould() { }

        [Fact]
        public async Task GivenAnExistingTagWithNew_WhenHandled_WillSucceed()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var updateRequest = new TodoTagRequest() { Id = createResponse.Id, Name = "Bar" };
            var updateResponse = await _mediator.Send(new UpdateTodoTagCommand(updateRequest));

            updateResponse.Id.Should().Be(createResponse.Id);
            updateResponse.Name.Should().Be(updateRequest.Name);
        }

        [Fact]
        public async Task GivenTagIdThatDoesNotExist_WhenHandled_WillThrowEntityNotFoundException()
        {
            var request = new TodoTagRequest() { Id = Guid.NewGuid(), Name = "Foo" };
            var action = async () => await _mediator.Send(new UpdateTodoTagCommand(request));

            await action.Should().ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GivenUpdatedTagNameThatAlreadyExists_WhenHandled_WillThrowException()
        {
            var firstCreateRequest = new TodoTagRequest() { Name = "Foo" };
            var firstCreateResponse = await _mediator.Send(new CreateTodoTagCommand(firstCreateRequest));

            var secondCreateRequest = new TodoTagRequest() { Name = "Bar" };
            await _mediator.Send(new CreateTodoTagCommand(secondCreateRequest));

            var updateRequest = new TodoTagRequest() { Id = firstCreateResponse.Id, Name = "bar" };
            var action = async () => await _mediator.Send(new UpdateTodoTagCommand(updateRequest));

            await action.Should().ThrowAsync<DuplicateTagException>();
        }

        [Fact]
        public async Task GivenUpdatedDuplicateTagNameOfExistingTag_WhenHandled_WillSucceed()
        {
            var firstCreateRequest = new TodoTagRequest() { Name = "Foo" };
            await _mediator.Send(new CreateTodoTagCommand(firstCreateRequest));

            var secondCreateRequest = new TodoTagRequest() { Name = "Bar" };
            var secondCreateResponse = await _mediator.Send(new CreateTodoTagCommand(secondCreateRequest));

            var updateRequest = new TodoTagRequest() { Id = secondCreateResponse.Id, Name = "bar" };
            var updateResponse = await _mediator.Send(new UpdateTodoTagCommand(updateRequest));

            updateResponse.Id.Should().Be(secondCreateResponse.Id);
            updateResponse.Name.Should().Be(updateRequest.Name);
        }

        [Fact]
        public async Task GivenUpdatedDuplicateTagNameOfExistingTag_WhenHandled_WillSucceedAndTrimWhitespace()
        {
            var firstCreateRequest = new TodoTagRequest() { Name = "Foo" };
            await _mediator.Send(new CreateTodoTagCommand(firstCreateRequest));

            var secondCreateRequest = new TodoTagRequest() { Name = "Bar" };
            var secondCreateResponse = await _mediator.Send(new CreateTodoTagCommand(secondCreateRequest));

            var updateRequest = new TodoTagRequest() { Id = secondCreateResponse.Id, Name = " bar " };
            var updateResponse = await _mediator.Send(new UpdateTodoTagCommand(updateRequest));
            var trimmedName = updateRequest.Name.Trim();

            updateResponse.Id.Should().Be(secondCreateResponse.Id);
            updateResponse.Name.Should().Be(trimmedName);
        }

        [Fact]
        public async Task GivenUpdateOfExistingTagWithoutName_WhenHandled_WillThrowException()
        {
            var firstCreateRequest = new TodoTagRequest() { Name = "Foo" };
            var firstCreateResponse = await _mediator.Send(new CreateTodoTagCommand(firstCreateRequest));

            var updateRequest = new TodoTagRequest() { Id = firstCreateResponse.Id, Name = "" };
            var action = async () => await _mediator.Send(new UpdateTodoTagCommand(updateRequest));

            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
