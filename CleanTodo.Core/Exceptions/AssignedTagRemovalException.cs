using CleanTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Exceptions
{
    public class AssignedTagRemovalException : Exception
    {
        public AssignedTagRemovalException(string message) : base(message) { }
    }
}
