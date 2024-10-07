using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;
using PayrollSystem.Repository;
using PayrollSystem.Exceptions;
using PayrollSystem.Services;
using PayrollSystem.Services.Interfaces;

namespace PayrollSystem.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IPayrollService _payrollService;
        private readonly IPayrollRepository _payrollRepository;


        public TaxService(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
            
        }

        public TaxService()
            {
                _taxRepository = new TaxRepository();
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            try
            {
                decimal totaltax = _taxRepository.CalculateTax(employeeId, taxYear);
                return totaltax;
            }
            catch (InvalidInputException ex1)
            {
                Console.WriteLine($"Error: {ex1.Message}");
                return 0;
            }
            catch (TaxCalculationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }

        }

        public Tax GetTaxById(int taxId)
        {
            return _taxRepository.GetTaxById(taxId);
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            return _taxRepository.GetTaxesForEmployee(employeeId);
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            return _taxRepository.GetTaxesForYear(taxYear);
        }
    }
}
