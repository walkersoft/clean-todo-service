using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoItems;
using CleanTodo.Core.Entities;
using MediatR;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record SetTodoItemCompletionStatusCommand(Guid Id, bool IsComplete) : IRequest<TodoItemResponse>;

    public class SetTodoItemCompletionStatusCommandHandler : IRequestHandler<SetTodoItemCompletionStatusCommand, TodoItemResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SetTodoItemCompletionStatusCommandHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoItemResponse> Handle(SetTodoItemCompletionStatusCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.FirstOrNotFound<TodoItem>(request.Id);
            todoItem.IsComplete = request.IsComplete;
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoItemResponse>(todoItem);
        }
    }
}
