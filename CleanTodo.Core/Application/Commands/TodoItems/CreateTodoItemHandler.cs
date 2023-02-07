using AutoMapper;
using CleanTodo.Core.Application.Interfaces;
using CleanTodo.Core.Entities;
using MediatR;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record CreateTodoItemCommand(CreateTodoItemRequest Data) : IRequest<TodoItemResponse>;

    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, TodoItemResponse>
    {
        //private readonly ITodoApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTodoItemHandler(IMapper mapper) //ITodoApplicationDbContext dbContext, IMapper mapper)
        {
            //_dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<TodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = _mapper.Map<TodoItem>(request.Data);
            return Task.FromResult(new TodoItemResponse { Id = Guid.NewGuid() });
        }
    }
}
