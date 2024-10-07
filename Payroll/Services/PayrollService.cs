using System;
using System.Collections.Generic;
using PayrollSystem.Model;
using PayrollSystem.Services.Interfaces;
using PayrollSystem.Exceptions;
using PayrollSystem.Repository;

namespace PayrollSystem.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;

        public PayrollService(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }
        public PayrollService()
        {
            _payrollRepository = new PayrollRepository();
        }

        public int AddPayroll(Payroll payroll)
        {
            return _payrollRepository.AddPayroll(payroll);
        }
        public Payroll GetPayrollById(int payrollId)
        {
            if (payrollId <= 0)
            {
                throw new Exception("Invalid payroll ID.");
            }

            // Assume there’s logic here to retrieve payroll from the database
            Payroll payroll = _payrollRepository.GetPayrollById(payrollId);

            if (payroll == null)
            {
                throw new Exception("Payroll not found.");
            }

            return payroll;
        }


        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            return _payrollRepository.GetPayrollsForEmployee(employeeId);
        }
        public decimal CalculateTotalNetSalary(int employeeId)
        {
            var payrollRecords = _payrollRepository.GetPayrollsForEmployee(employeeId);
            var totalNetSalary = 0m;
            foreach (var record in payrollRecords)
            {
                totalNetSalary += record.NetSalary;
            }
            return totalNetSalary;
        }
        public List<Payroll> GetPayrollsForPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate)
        {
            return _payrollRepository.GetPayrollsForPeriod(payPeriodStartDate, payPeriodEndDate);

        }
        public List<Payroll> GetPayrollsForEmployeeInYear(int employeeId, int year)
        {
            try
            {
                // Call the repository method to get payrolls
                var payrolls = _payrollRepository.GetPayrollsForEmployeeInYear(employeeId, year);
                return payrolls;
            }
            catch (Exception ex)
            {
                // Log the exception (implementation not shown)
                throw new Exception($"An error occurred while retrieving payrolls for employee {employeeId} in year {year}: {ex.Message}", ex);
            }
        }

        public decimal CalculateTotalNetSalaryForEmployeeInYear(int employeeId, int year)
        {
            try
            {
                // Get payroll records for the specified employee and year
                var payrollRecords = GetPayrollsForEmployeeInYear(employeeId, year);
                decimal totalNetSalary = 0m;

                // Calculate the total net salary
                foreach (var payroll in payrollRecords)
                {
                    totalNetSalary += payroll.NetSalary;
                }

                return totalNetSalary;
            }
            catch (Exception ex)
            {
                // Log the exception (implementation not shown)
                throw new Exception($"An error occurred while calculating total net salary for employee {employeeId} in year {year}: {ex.Message}", ex);
            }
        }
    }
}
