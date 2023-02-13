using AutoMapper;
using CleanTodo.Core.Application.Interfaces.Mapping;
using CleanTodo.Core.Application.Queries.TodoItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Entities
{
    public class TodoItem : BaseEntity, IMapTo<TodoItemResponse>, IProjectTo<TodoItem, ProjectedTodoItemResponse>
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        public bool RollsOver { get; set; }
        public int RollOverCount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<TodoTag> Tags { get; private set; }

        public TodoItem()
        {
            Tags = new List<TodoTag>();
        }

        public void ConfigureMapping(IProfileExpression profile)
        {
            profile.CreateMap<TodoItem, TodoItemResponse>()
                .ForMember(
                    source => source.Tags,
                    dest => dest.MapFrom(x => x.Tags.Select(t => t.Id))
                );
        }

        public void ConfigureProjection(IProfileExpression profile)
        {
            profile.CreateProjection<TodoItem, ProjectedTodoItemResponse>()
                .ForMember(
                    source => source.Tags, 
                    dest => dest.MapFrom(x => x.Tags.Select(t => t.Id))
                );
        }
    }
}
