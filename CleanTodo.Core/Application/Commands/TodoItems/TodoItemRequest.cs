using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Entities;

namespace CleanTodo.Core.Application.Commands.TodoItems
{
    public class TodoItemRequest : IMapTo<TodoItem>
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool RollsOver { get; set; }
        public DateTime DueDate { get; set; }
        public ICollection<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
