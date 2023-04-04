using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTodo.Core.Exceptions
{
    public class DuplicateTagException : Exception
    {
        public DuplicateTagException(string message) : base(message) { }
    }
}
