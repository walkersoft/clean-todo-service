using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoList : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<TodoItem> TodoItems { get; private set; }

        public TodoList()
        {
            TodoItems = new List<TodoItem>();
        }
    }
}
