using PayrollSystem.Model;
using PayrollSystem.Services.Interfaces;
using PayrollSystem.Exceptions;
using PayrollSystem.Repository;
using System;
using System.Collections.Generic;

namespace PayrollSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        // Constructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public void AddEmployee(Employee employeeData)
        {
            int addEmployeeStatus = _employeeRepository.AddEmployee(employeeData);
            if (addEmployeeStatus > 0)
            {
                Console.WriteLine("Employee added successfully.");
            }
            else
            {
                Console.WriteLine("Addition failed.");
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        public void UpdateEmployee(Employee employeeData)
        {
            int updateStatus = _employeeRepository.UpdateEmployee(employeeData);
            if (updateStatus > 0)
            {
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Update failed.");
            }
        }

        public void RemoveEmployee(int employeeId)
        {
            bool removalStatus = _employeeRepository.RemoveEmployee(employeeId);
            if (removalStatus)
            {
                Console.WriteLine("Employee removed successfully.");
            }
            else
            {
                Console.WriteLine("Removal failed.");
            }
        }

        public bool EmployeeExists(int employeeId)
        {
            var employee = GetEmployeeById(employeeId); 
            return employee != null; 
        }

    }
}
