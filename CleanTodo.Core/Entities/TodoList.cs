using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Application.Queries.TodoLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoList : BaseEntity, IMapTo<TodoListResponse>
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

        public void ConfigureMapping(IProfileExpression profile)
        {
            profile.CreateMap<TodoList, TodoListResponse>()
                .ForMember(
                    destination => destination.TodoItems,
                    source => source.MapFrom(todoList => todoList.TodoItems.Select(item => item.Id))
                );
        }
    }
}
