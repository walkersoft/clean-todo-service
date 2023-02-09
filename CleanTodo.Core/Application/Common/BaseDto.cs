using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Application.Common
{
    abstract public class BaseDto
    {
        public Guid Id { get; set; }
    }
}
