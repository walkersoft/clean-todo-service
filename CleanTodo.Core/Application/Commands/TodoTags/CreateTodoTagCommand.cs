using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoTags
{
    public record CreateTodoTagCommand(TodoTagRequest Data) : IRequest<TodoTagResponse>;

    public class CreateTodoTagCommandHandler : IRequestHandler<CreateTodoTagCommand, TodoTagResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTodoTagCommandHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoTagResponse> Handle(CreateTodoTagCommand request, CancellationToken cancellationToken)
        {
            // See if this tag already exists, ignoring case-sensitivity
            var tag = _context.TodoTags
                .Where(tag => request.Data.Name.ToLower().Trim() == tag.Name.ToLower().Trim())
                .FirstOrDefault();

            // If the tag does not exist, create it
            if (tag == null)
            {
                tag = _mapper.Map<TodoTag>(request.Data);
                tag.Name = tag.Name.Trim();
                _context.TodoTags.Add(tag);
                await _context.SaveChangesAsync();
            }            

            return _mapper.Map<TodoTagResponse>(tag);
        }
    }
}
