using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Core.Application.Queries.TodoLists
{
    public record GetAllTodoListsQuery : IRequest<IEnumerable<TodoListResponse>>;

    public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, IEnumerable<TodoListResponse>>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTodoListsQueryHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoListResponse>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
        {
            var lists = await _context.TodoLists
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return lists.Select(list => _mapper.Map<TodoListResponse>(list));
        }
    }
}
