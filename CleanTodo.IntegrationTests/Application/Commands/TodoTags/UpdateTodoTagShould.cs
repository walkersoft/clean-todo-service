﻿using CleanTodo.Core.Application.Commands.TodoTags;
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

        //[Fact]
        public async Task GivenAnExistingTagWithNew_WhenHandled_WillSucceed()
        {
            var createRequest = new TodoTagRequest() { Name = "Foo" };
            var createResponse = await _mediator.Send(new CreateTodoTagCommand(createRequest));

            var updateRequest = new TodoTagRequest() { Id = createResponse.Id, Name = "Bar" };
            
        }
    }
}
