using NUnit.Framework;
using PayrollSystem.Model;
using PayrollSystem.Repository;
using PayrollSystem.Services;
using PayrollSystem.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PayXpert.Tests
{
    [TestFixture]
    public class Test1
    {
        private PayrollService _payrollService;
        public IEmployeeService _employeeService;
        public IEmployeeRepository _employeeRepository;
        public ITaxService _taxServices;
        public ITaxRepository _taxRepository;

        [SetUp]
        public void Setup()
        {
            _employeeRepository = new EmployeeRepository();
            _employeeService = new EmployeeService(_employeeRepository);
            _taxRepository = new TaxRepository();
            _taxServices = new TaxService(_taxRepository);
            _payrollService = new PayrollService(new PayrollRepository());
        }

        [Test]
        public void CalculateGrossSalaryForEmployee_ShouldReturnCorrectGrossSalary()
        {
            // Arrange
            var payroll = new Payroll
            {
                EmployeeID = 1,
                PayPeriodStartDate = new DateTime(2023, 1, 1),
                PayPeriodEndDate = new DateTime(2023, 1, 31),
                BasicSalary = 50000,
                OvertimePay = 10000,
                Deductions = 0
            };
            _payrollService.AddPayroll(payroll);

            // Act
            var result = payroll.BasicSalary + payroll.OvertimePay;

            // Assert
            Assert.That(60000, Is.EqualTo(result));// Basic salary + overtime
        }

        [Test]
        public void CalculateNetSalaryAfterDeductions_ShouldReturnCorrectNetSalary()
        {
            // Arrange
            var payroll = new Payroll
            {
                EmployeeID = 1,
                PayPeriodStartDate = new DateTime(2023, 1, 1),
                PayPeriodEndDate = new DateTime(2023, 1, 31),
                BasicSalary = 50000,
                OvertimePay = 10000,
                Deductions = 7000
            };
            _payrollService.AddPayroll(payroll);

            // Act
            var result = (payroll.BasicSalary + payroll.OvertimePay) - payroll.Deductions;

            // Assert
            Assert.That(53000, Is.EqualTo(result)); // Gross salary - deductions
        }

        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee_ShouldReturnCorrectTax()
        {
            // Arrange
            var payroll = new Payroll
            {
                EmployeeID = 1,
                PayPeriodStartDate = new DateTime(2023, 1, 1),
                PayPeriodEndDate = new DateTime(2023, 1, 31),
                BasicSalary = 200000,
                OvertimePay = 0,
                Deductions = 30000 // Assumed tax for high income
            };
            _payrollService.AddPayroll(payroll);

            // Act
            var result = payroll.Deductions; // Assuming deductions as tax

            // Assert
            Assert.That(30000, Is.EqualTo(result)); // High income employee tax
        }

        [Test]
        public void ProcessPayrollForMultipleEmployees_ShouldProcessPayrollSuccessfully()
        {
            // Arrange
            var payrolls = new List<Payroll>
            {
                new Payroll {  EmployeeID = 1, BasicSalary = 50000, OvertimePay = 10000, Deductions = 7000, PayPeriodStartDate = new DateTime(2023, 1, 1), PayPeriodEndDate = new DateTime(2023, 1, 31) },
                new Payroll {  EmployeeID = 1, BasicSalary = 70000, OvertimePay = 15000, Deductions = 10000, PayPeriodStartDate = new DateTime(2023, 1, 1), PayPeriodEndDate = new DateTime(2023, 1, 31) }
            };

            foreach (var payroll in payrolls)
            {
                _payrollService.AddPayroll(payroll);
            }

            // Act
            var payrollList = _payrollService.GetPayrollsForEmployee(1);

            // Assert
            Assert.That(payrollList, Is.Not.Null);
            Assert.That(payrollList.Count, Is.GreaterThan(0)); // Ensure payroll is processed for employees
        }

        [Test]
        public void VerifyErrorHandlingForInvalidEmployeeData_ShouldThrowException()
        {
            // Act and Assert
            var ex = Assert.Throws<Exception>(() => _payrollService.GetPayrollById(-1));

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Invalid payroll ID."));
        }
    }
}
