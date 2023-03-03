using CleanTodo.Core.Application.Common;
using CleanTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Queries.TodoTags
{
    public class TodoTagResponse : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsAssigned { get; set; }
        public int AssignedCount { get; set; }

        public void SetAssignments(TodoTag fromTag)
        {
            IsAssigned = fromTag.TodoItems.Count > 0;
            AssignedCount = fromTag.TodoItems.Count;
        }
    }
}
