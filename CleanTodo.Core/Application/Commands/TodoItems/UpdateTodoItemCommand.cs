using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Entities;
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
            var todoItem = await _context.FirstOrNotFound<TodoItem>(request.Data.Id);
            todoItem.Description = request.Data.Description;
            todoItem.DueDate = request.Data.DueDate;
            todoItem.IsActive = request.Data.IsActive;
            todoItem.RollsOver  = request.Data.RollsOver;
            todoItem.Tags.Clear();

            var tags = await _context.TodoTags
                .Where(t => request.Data.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

            tags.ForEach(tag => todoItem.Tags.Add(tag));

            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoItemResponse>(todoItem);            
        }
    }
}
