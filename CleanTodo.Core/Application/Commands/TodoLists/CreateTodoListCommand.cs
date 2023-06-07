using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using CleanTodo.Core.Application.Queries.TodoLists;
using CleanTodo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoLists
{
    public record CreateTodoListCommand(TodoListRequest Data) : IRequest<TodoListResponse>;

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoListResponse>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTodoListCommandHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoListResponse> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = _mapper.Map<TodoList>(request.Data);
            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoListResponse>(todoList);
        }
    }
}
