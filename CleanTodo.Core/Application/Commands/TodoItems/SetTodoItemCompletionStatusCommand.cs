using CleanTodo.Core.Application.Queries.TodoItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record SetTodoItemCompletionStatusCommand(Guid Id, bool IsComplete) : IRequest<TodoItemResponse>;

    public class SetTodoItemCompletionStatusCommandHandler : IRequestHandler<SetTodoItemCompletionStatusCommand, TodoItemResponse>
    {
        public Task<TodoItemResponse> Handle(SetTodoItemCompletionStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
