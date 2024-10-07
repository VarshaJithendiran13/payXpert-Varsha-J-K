using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Model
{
    public class Employee
    {
        // Properties with getters and setters, initialized with default values
        public int EmployeeID { get; set; }
        public string FirstName { get; set; } = string.Empty;  // Initialize with default value
        public string LastName { get; set; } = string.Empty;   // Initialize with default value
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;     // Initialize with default value
        public string Email { get; set; } = string.Empty;      // Initialize with default value
        public string PhoneNumber { get; set; } = string.Empty; // Initialize with default value
        public string Address { get; set; } = string.Empty;    // Initialize with default value
        public string Position { get; set; } = string.Empty;   // Initialize with default value
        public DateTime JoiningDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        // Default Constructor
        public Employee() { }

        // Parametrized Constructor
        public Employee(int employeeID, string firstName, string lastName, DateTime dateOfBirth, string gender, string email,
            string phoneNumber, string address, string position, DateTime joiningDate, DateTime? terminationDate = null)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Position = position;
            JoiningDate = joiningDate;
            TerminationDate = terminationDate;
        }

        // Method to calculate the age
        public int CalculateAge()
        {
            var age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > DateTime.Now.AddYears(-age)) age--;
            return age;
        }
    }
}
