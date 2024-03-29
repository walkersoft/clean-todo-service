﻿using CleanTodo.Api.Middleware.Exceptions;
using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Queries.TodoItems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<TodoItemResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllTodoItemsQuery()));
        }

        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Create(TodoItemRequest request)
        {
            return Ok(await _mediator.Send(new CreateTodoItemCommand(request)));
        }

        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Update(TodoItemRequest request)
        {
            return Ok(await _mediator.Send(new UpdateTodoItemCommand(request)));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTodoItemCommand(id));
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [HttpPut("SetCompletion")]
        public async Task<IActionResult> SetCompletion(Guid id, bool completionStatus)
        {
            return Ok(await _mediator.Send(new SetTodoItemCompletionStatusCommand(id, completionStatus)));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [HttpPut("AssignToList")]
        public async Task<IActionResult> AssignToList(AssignTodoItemRequest request)
        {
            await _mediator.Send(new AssignTodoItemCommand(request));
            return NoContent();
        }
    }
}
