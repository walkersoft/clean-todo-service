using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Queries.TodoTags
{
    public record GetAllTodoTagsQuery() : IRequest<IEnumerable<TodoTagResponse>>;

    public class GetAllTodoTagsQueryHandler : IRequestHandler<GetAllTodoTagsQuery, IEnumerable<TodoTagResponse>>
    {
        public Task<IEnumerable<TodoTagResponse>> Handle(GetAllTodoTagsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
