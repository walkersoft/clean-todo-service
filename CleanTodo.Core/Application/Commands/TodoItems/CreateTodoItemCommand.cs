using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Entities;
using MediatR;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record CreateTodoItemCommand(TodoItemRequest Data) : IRequest<TodoItemResponse>;

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(ITodoApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<TodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _mapper.Map<TodoItem>(request.Data);
            _context.TodoTags
                .Where(tag => request.Data.TagIds.Contains(tag.Id))
                .ToList()
                .ForEach(tag => todoItem.Tags.Add(tag));

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoItemResponse>(todoItem);
        }
    }
}
