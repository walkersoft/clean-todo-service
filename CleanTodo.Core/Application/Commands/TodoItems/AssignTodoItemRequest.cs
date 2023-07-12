using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public sealed class AssignTodoItemRequest
    {
        public Guid TodoItemId  { get; set; }
        public Guid TodoListId { get; set; }
    }
}
