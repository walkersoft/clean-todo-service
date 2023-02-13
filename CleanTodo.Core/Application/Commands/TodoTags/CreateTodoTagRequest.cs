using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Commands.TodoTags
{
    public class CreateTodoTagRequest : IMapTo<TodoTag>
    {
        public string Name { get; set; } = string.Empty;
    }
}
