using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record DeleteTodoItemCommand(Guid Id) : IRequest<Unit>;

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Unit>
    {
        public Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
