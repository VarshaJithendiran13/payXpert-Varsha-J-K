using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Exceptions
{
    public class PayrollGenerationException : Exception
    {
        public PayrollGenerationException()
            : base("Error generating payroll for the employee.") { }

        public PayrollGenerationException(string message)
            : base(message) { }

        public PayrollGenerationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

