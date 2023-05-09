using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanTodo.Core.Application.Interfaces.Persitence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Core.Application.Queries.TodoItems
{
    public record GetAllTodoItemsQuery : IRequest<IEnumerable<TodoItemResponse>>;

    public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IEnumerable<TodoItemResponse>>
    {
        private readonly ITodoApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllTodoItemsQueryHandler(ITodoApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemResponse>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _dbContext.TodoItems
                .AsNoTracking()
                .ProjectTo<ProjectedTodoItemResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            CalculateRollOverQuantities(items.Where(item => item.RollsOver));

            return items;
        }

        private static void CalculateRollOverQuantities(IEnumerable<ProjectedTodoItemResponse> rollingItems)
        {
            var today = DateTime.Today;
            foreach (var rollingItem in rollingItems)
            {
                if (rollingItem.DueDate < today)
                {
                    rollingItem.RollOverCount = (today - rollingItem.DueDate).Days;
                }
            }
        }
    }
}
