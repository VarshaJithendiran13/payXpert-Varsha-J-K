using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Exceptions
{
    public class FinancialRecordException : Exception
    {
        public FinancialRecordException()
            : base("Error managing financial record.") { }

        public FinancialRecordException(string message)
            : base(message) { }

        public FinancialRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
