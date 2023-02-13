using CleanTodo.Core.Application.Common;
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
    }
}
