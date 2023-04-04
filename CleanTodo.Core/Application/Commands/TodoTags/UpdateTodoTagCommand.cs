using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            // Verify the tag exists
            var tag = await _context.FirstOrNotFound<TodoTag>(request.Data.Id);

            // See if this tag already exists, ignoring case-sensitivity
            var duplicateCheck = await _context.TodoTags
                .Where(tag => request.Data.Name.ToLower().Trim() == tag.Name.ToLower().Trim())
                .SingleOrDefaultAsync(cancellationToken);

            if (duplicateCheck != null && duplicateCheck.Id != request.Data.Id)
            {
                throw new DuplicateTagException(string.Format(
                    "Unabled to edit tag with Id: {0} to name: {1}. A tag with this name already exists.",
                    request.Data.Id,
                    request.Data.Name
                ));
            }

            tag.Name = request.Data.Name;
            _context.TodoTags.Update(tag);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoTagResponse>(tag);
        }
    }
}
