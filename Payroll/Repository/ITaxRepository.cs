using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;


namespace PayrollSystem.Repository

{
    public interface ITaxRepository
    {
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);
        //void AddTax(Tax tax);
        int AddTax(Tax tax);
        decimal CalculateTax(int employeeId, int taxYear);


    }
}
