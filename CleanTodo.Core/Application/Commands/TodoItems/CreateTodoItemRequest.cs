using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public class CreateTodoItemRequest
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool RollsOver { get; set; }
        public DateTime DateDate { get; set; }
        public ICollection<Guid> Tags { get; set; } = new List<Guid>();
    }
}
