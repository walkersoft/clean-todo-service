using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Application.Queries.TodoTags;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoTag : BaseEntity, IMapTo<TodoTagResponse>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<TodoItem> TodoItems { get; private set; }

        public TodoTag()
        {
            TodoItems = new List<TodoItem>();
        }
    }

    public class TodoTagValidator : AbstractValidator<TodoTag>
    {
        public TodoTagValidator()
        {
            RuleFor(tag => tag.Name).NotEmpty();
        }
    }
}
