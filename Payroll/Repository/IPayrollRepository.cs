using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;

namespace PayrollSystem.Repository
{
    public interface IPayrollRepository
    {
        int AddPayroll(Payroll payroll);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate);
        List<Payroll> GetPayrollsForEmployeeInYear(int employeeId, int year);
        
    }
}


