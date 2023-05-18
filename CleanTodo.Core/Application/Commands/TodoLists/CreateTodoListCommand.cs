using CleanTodo.Core.Application.Queries.TodoLists;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoLists
{
    public record CreateTodoListCommand(TodoListRequest Data) : IRequest<TodoListResponse>;

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoListResponse>
    {
        public Task<TodoListResponse> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
