using CleanTodo.Core.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public class TodoItemResponse : BaseDto
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        public bool RollsOver { get; set; }
        public int RollOverCount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<Guid> Tags { get; set; } = new List<Guid>();
    }
}
