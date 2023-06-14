using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Queries.TodoLists
{
    public record GetAllTodoListsQuery : IRequest<IEnumerable<TodoListResponse>>;

    public class GetAllTodoListsQueryHandler : IRequestHandler<GetAllTodoListsQuery, IEnumerable<TodoListResponse>>
    {
        public Task<IEnumerable<TodoListResponse>> Handle(GetAllTodoListsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
