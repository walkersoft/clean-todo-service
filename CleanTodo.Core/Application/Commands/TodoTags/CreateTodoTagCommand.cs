using CleanTodo.Core.Application.Queries.TodoTags;
using CleanTodo.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoTags
{
    public record CreateTodoTagCommand(CreateTodoTagRequest Data) : IRequest<TodoTagResponse>;

    public class CreateTodoTagCommandHandler : IRequestHandler<CreateTodoTagCommand, TodoTagResponse>
    {
        public Task<TodoTagResponse> Handle(CreateTodoTagCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
