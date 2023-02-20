using CleanTodo.Api.Middleware.Exceptions;
using CleanTodo.Core.Application.Commands.TodoTags;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Application.Queries.TodoTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<TodoTagResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllTodoTagsQuery()));
        }

        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Create(TodoTagRequest request)
        {
            return Ok(await _mediator.Send(new CreateTodoTagCommand(request)));
        }

        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Update(TodoTagRequest request)
        {
            return Ok(await _mediator.Send(new UpdateTodoTagCommand(request)));
        }
    }
}
