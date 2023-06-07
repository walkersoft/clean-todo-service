using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoLists
{
    public class TodoListRequest : IMapTo<TodoList>
    {
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}
