using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Entities;
using MediatR;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public record AssignTodoItemCommand(AssignTodoItemRequest Data) : IRequest<Unit>;

    public class AssignTodoItemCommandHandler : IRequestHandler<AssignTodoItemCommand, Unit>
    {
        private readonly ITodoApplicationDbContext _context;

        public AssignTodoItemCommandHandler(ITodoApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AssignTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _context.FirstOrNotFound<TodoItem>(request.Data.TodoItemId);
            var todoList = await _context.FirstOrNotFound<TodoList>(request.Data.TodoListId);

            todoList.TodoItems.Add(todoItem);
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return await Unit.Task;
        }
    }
}