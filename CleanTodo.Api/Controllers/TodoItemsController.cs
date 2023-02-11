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

        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoItemRequest request)
        {
            return Ok(await _mediator.Send(new CreateTodoItemCommand(request)));
        }
    }
}
