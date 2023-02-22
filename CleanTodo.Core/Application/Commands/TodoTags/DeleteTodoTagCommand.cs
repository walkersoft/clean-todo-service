using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoTags
{
    public record DeleteTodoTagCommand(Guid Id) : IRequest<Unit>;

    public class DeleteTodoTagCommandHandler : IRequestHandler<DeleteTodoTagCommand, Unit>
    {
        private readonly ITodoApplicationDbContext _context;

        public DeleteTodoTagCommandHandler(ITodoApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.FirstOrNotFound<TodoTag>(request.Id);
            _context.TodoTags.Remove(tag);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
