using CleanTodo.Core.Application.Commands.TodoItems;
using CleanTodo.Core.Application.Interfaces.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoItem : BaseEntity, IMapTo<TodoItemResponse>
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        public bool RollsOver { get; set; }
        public int RollOverCount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<TodoTag> Tags { get; private set; }

        public TodoItem()
        {
            Tags = new List<TodoTag>();
        }
    }
}
