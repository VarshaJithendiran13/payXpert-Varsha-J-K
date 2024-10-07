using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Exceptions
{
    public class TaxCalculationException : Exception
    {
        public TaxCalculationException()
            : base("Error calculating taxes for the employee.") { }

        public TaxCalculationException(string message)
            : base(message) { }

        public TaxCalculationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

