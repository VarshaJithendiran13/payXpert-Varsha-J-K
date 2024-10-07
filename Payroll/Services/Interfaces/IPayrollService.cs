using PayrollSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Services.Interfaces
{
    public interface IPayrollService
    {
        int AddPayroll(Payroll payroll);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate);
        decimal CalculateTotalNetSalary(int employeeId);
        List<Payroll> GetPayrollsForEmployeeInYear(int employeeId, int year);
        decimal CalculateTotalNetSalaryForEmployeeInYear(int employeeId, int year);
    }
}

