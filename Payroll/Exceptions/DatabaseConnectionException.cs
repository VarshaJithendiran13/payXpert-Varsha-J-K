using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException()
            : base("Error establishing or maintaining a database connection.") { }

        public DatabaseConnectionException(string message)
            : base(message) { }

        public DatabaseConnectionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

