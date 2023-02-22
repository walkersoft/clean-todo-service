using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
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
    public record UpdateTodoTagCommand(TodoTagRequest Data) : IRequest<TodoTagResponse>;

    public class UpdateTodoTagCommandHandler : IRequestHandler<UpdateTodoTagCommand, TodoTagResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTodoTagCommandHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoTagResponse> Handle(UpdateTodoTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.FirstOrNotFound<TodoTag>(request.Data.Id);
            tag.Name = request.Data.Name;
            _context.TodoTags.Update(tag);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoTagResponse>(tag);
        }
    }
}
