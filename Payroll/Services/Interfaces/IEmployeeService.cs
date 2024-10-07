using PayrollSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Services;
using PayrollSystem.Repository;

namespace PayrollSystem.Services.Interfaces
{
    public interface IEmployeeService
        {
            Employee GetEmployeeById(int employeeId);
            List<Employee> GetAllEmployees();
            void AddEmployee(Employee employeeData);
            void UpdateEmployee(Employee employeeData);
            void RemoveEmployee(int employeeId);
        }
}

