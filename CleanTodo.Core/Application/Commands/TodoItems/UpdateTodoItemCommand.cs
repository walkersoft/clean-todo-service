using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Entities;
using CleanTodo.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record UpdateTodoItemCommand(TodoItemRequest Data) : IRequest<TodoItemResponse>;

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITodoApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IMapper mapper, ITodoApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TodoItemResponse> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.TodoItems
                .Where(x => x.Id == request.Data.Id)
                .Include(x => x.Tags)
                .SingleOrDefaultAsync(cancellationToken);

            if (todoItem == null)
            {
                throw new EntityNotFoundException(string.Format(
                    "Unable to locate entity of type: {0} with ID: {1} in the database.",
                    typeof(TodoItem),
                    request.Data.Id)
                );
            }

            todoItem.Description = request.Data.Description;
            todoItem.DueDate = request.Data.DueDate.Date;
            todoItem.IsActive = request.Data.IsActive;
            todoItem.RollsOver  = request.Data.RollsOver;
            todoItem.Tags.Clear();
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            var tags = await _context.TodoTags
                .Where(t => request.Data.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

            tags.ForEach(tag => todoItem.Tags.Add(tag));

            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            var mapped = _mapper.Map<TodoItemResponse>(todoItem);
            return mapped;
        }
    }
}
