using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoTags
{
    public record UnassignTodoTagCommand(TodoTagRequest Data) : IRequest<TodoTagResponse>;

    public class UnassignTodoTagCommandHandler : IRequestHandler<UnassignTodoTagCommand, TodoTagResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnassignTodoTagCommandHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoTagResponse> Handle(UnassignTodoTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.FirstOrNotFound<TodoTag>(request.Data.Id);
            var itemsWithTag = await _context.TodoItems
                .Where(x => x.Tags.Contains(tag))
                .ToListAsync(cancellationToken);

            itemsWithTag.ForEach(x => x.Tags.Remove(tag));
            _context.TodoItems.UpdateRange(itemsWithTag);

            await _context.SaveChangesAsync();

            var updatedTag = _mapper.Map<TodoTagResponse>(tag);
            updatedTag.SetAssignments(tag);

            return updatedTag;
        }
    }
}
