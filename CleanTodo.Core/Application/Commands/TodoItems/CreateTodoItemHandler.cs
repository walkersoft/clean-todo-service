using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Entities;
using MediatR;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record CreateTodoItemCommand(CreateTodoItemRequest Data) : IRequest<TodoItemResponse>;

    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, TodoItemResponse>
    {
        private readonly ITodoApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTodoItemHandler(ITodoApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _mapper.Map<TodoItem>(request.Data);
            _dbContext.TodoItems.Add(todoItem);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TodoItemResponse>(todoItem);
        }
    }
}
