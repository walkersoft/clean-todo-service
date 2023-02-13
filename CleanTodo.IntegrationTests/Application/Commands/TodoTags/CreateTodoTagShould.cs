using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using FluentAssertions;
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
        public CreateTodoTagShould() : base()
        {
        }

        [Fact]
        public async Task GivenValidTodoTagRequest_WhenHandled_WillSucceed()
        {
            var createRequest = new CreateTodoTagRequest() { Name = "Foo" };

            var response = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            response.Id.Should().NotBe(default(Guid));
            response.Name.Should().Be(createRequest.Name);
        }

        [Fact]
        public async Task GivenDuplicateName_WhenHandled_WillReturnExistingTag()
        {
            var firstTagRequest = new CreateTodoTagRequest() { Name = "Foo" };
            var secondTagRequest = new CreateTodoTagRequest() { Name = "Foo" };

            var firstTagResponse = await _mediator.Send(new CreateTodoTagCommand(firstTagRequest));
            var secondTagResponse = await _mediator.Send(new CreateTodoTagCommand(secondTagRequest));
            var allTagsResponse = await _mediator.Send(new GetAllTodoTagsQuery());

            allTagsResponse.Should().NotBeEmpty();
            allTagsResponse.Should().HaveCount(1);
            allTagsResponse.First().Id
                .Should().Be(firstTagResponse.Id)
                .And.Be(secondTagResponse.Id);
        }
    }
}
