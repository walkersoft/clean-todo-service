using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            // Verify a tag with this name does not exist already
            if (await _context.GetExistingTagId(request.Data.Name) != Guid.Empty)
            {
                throw new DuplicateTagException(string.Format(
                    "Unable to create tag. A tag with name: {0} already exists.",
                    request.Data.Name
                ));
            }

            var tag = _mapper.Map<TodoTag>(request.Data);
            var validator = new TodoTagValidator();
            await validator.ValidateAndThrowAsync(tag, cancellationToken);

            tag.Name = tag.Name.Trim();
            _context.TodoTags.Add(tag);

            await _context.SaveChangesAsync();      

            return _mapper.Map<TodoTagResponse>(tag);
        }
    }
}
