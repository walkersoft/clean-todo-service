using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Persitence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTodo.Core.Application.Queries.TodoTags
{
    public record GetAllTodoTagsQuery : IRequest<IEnumerable<TodoTagResponse>>;

    public class GetAllTodoTagsQueryHandler : IRequestHandler<GetAllTodoTagsQuery, IEnumerable<TodoTagResponse>>
    {
        private readonly ITodoApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTodoTagsQueryHandler(ITodoApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoTagResponse>> Handle(GetAllTodoTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _context.TodoTags
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return tags.Select(tag => _mapper.Map<TodoTagResponse>(tag));
        }
    }
}
