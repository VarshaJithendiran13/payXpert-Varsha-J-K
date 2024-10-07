using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;

namespace PayrollSystem.Repository
{
    public interface IEmployeeRepository
    {
        int AddEmployee(Employee employee);
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        int UpdateEmployee(Employee employee);
        bool RemoveEmployee(int employeeId);
    }
}
