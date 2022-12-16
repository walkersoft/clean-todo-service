using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoTag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<TodoItem> TodoItems { get; private set; }

        public TodoTag()
        {
            TodoItems = new List<TodoItem>();
        }
    }
}
