using CleanTodo.Core.Application.Commands.TodoLists;
using CleanTodo.Core.Application.Queries.TodoLists;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<TodoListResponse>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllTodoListsQuery()));
        }

        [ProducesResponseType(typeof(TodoListResponse), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Create(TodoListRequest request)
        {
            return Ok(await _mediator.Send(new CreateTodoListCommand(request)));
        }
    }
}
