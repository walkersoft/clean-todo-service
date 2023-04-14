using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Entities;
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
        private readonly ITodoApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(ITodoApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.FirstOrNotFound<TodoItem>(request.Id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
