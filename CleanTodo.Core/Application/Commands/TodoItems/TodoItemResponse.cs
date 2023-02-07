using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public class TodoItemResponse
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public bool IsComplete { get; private set; }
        public bool RollsOver { get; private set; }
        public int RollOverCount { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? CompletionDate { get; private set; }
        public ICollection<Guid> Tags { get; private set; } = new List<Guid>();
    }
}
