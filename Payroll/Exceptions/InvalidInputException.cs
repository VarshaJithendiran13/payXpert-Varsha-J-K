using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException()
            : base("Invalid input provided.") { }

        public InvalidInputException(string message)
            : base(message) { }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

