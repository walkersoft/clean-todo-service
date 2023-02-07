using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record CreateTodoItemCommand(CreateTodoItemRequest Data) : IRequest<TodoItemResponse>;
    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, TodoItemResponse>
    {
        public Task<TodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TodoItemResponse { Id = Guid.NewGuid() });
        }
    }
}
