using CleanTodo.Core.Application.Commands.TodoTags;
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
            var request = new CreateTodoTagRequest() { Name = "Foo" };

            var response = await _mediator.Send(new CreateTodoTagCommand(request));

            response.Id.Should().NotBe(default(Guid));
            response.Name.Should().Be(request.Name);
        }
    }
}
