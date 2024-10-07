using PayrollSystem.Model;
using PayrollSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Services.Interfaces;
using PayrollSystem.Repository;

namespace PayrollSystem.Main
{
    public class mainModule
    {
        static EmployeeService employeeService = new EmployeeService();
        static PayrollService payrollService = new PayrollService();
        static TaxService taxService = new TaxService();
        static FinancialRecordService financialRecordService = new FinancialRecordService();

        public static void Main(string[] args)
        {
            bool continueApp = true;

            while (continueApp)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Payroll System");
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Processing");
                Console.WriteLine("3. Tax Calculation");
                Console.WriteLine("4. Financial Reporting");
                Console.WriteLine("5. Exit");

                Console.Write("\nChoose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ManageEmployeeMenu();
                        break;
                    case 2:
                        PayrollProcessingMenu();
                        break;
                    case 3:
                        TaxCalculationMenu();
                        break;
                    case 4:
                        GenerateFinancialReportsMenu();
                        break;
                    case 5:
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
        //Employee Management Menu
        #region EmployeeMenu
        // Employee Management Menu
        static void ManageEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("Employee Management:");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Delete Employee");
            Console.Write("\nChoose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    ViewAllEmployees();
                    break;
                case 3:
                    UpdateEmployee();
                    break;
                case 4:
                    DeleteEmployee();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // Employee CRUD operations using repository
        static void AddEmployee()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Gender (M/F): ");
            string gender = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Position: ");
            string position = Console.ReadLine();

            Console.Write("Enter Joining Date (yyyy-MM-dd): ");
            DateTime joiningDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Termination Date (yyyy-MM-dd) or press Enter if not applicable: ");
            string terminationInput = Console.ReadLine();
            DateTime? terminationDate = string.IsNullOrEmpty(terminationInput) ? (DateTime?)null : DateTime.Parse(terminationInput);

            // EmployeeID is set to 0 because it's auto-incremented in the database
            Employee employee = new Employee(0, firstName, lastName, dateOfBirth, gender, email, phoneNumber, address, position, joiningDate, terminationDate);

            employeeService.AddEmployee(employee);
            Console.WriteLine("Employee added successfully.");
        }
        static void ViewAllEmployees()
        {
            List<Employee> employees = employeeService.GetAllEmployees();

            Console.Clear();
            Console.WriteLine("List of All Employees:");

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.EmployeeID}, Name: {emp.FirstName} {emp.LastName}, Date of Birth: {emp.DateOfBirth.ToShortDateString()}, Gender: {emp.Gender}, Email: {emp.Email}, Phone: {emp.PhoneNumber}, Address: {emp.Address}, Position: {emp.Position}, Joining Date: {emp.JoiningDate.ToShortDateString()}, Termination Date: {(emp.TerminationDate.HasValue ? emp.TerminationDate.Value.ToShortDateString() : "N/A")}");
                }
            }

            Console.ReadLine();
        }

        static void UpdateEmployee()
        {
            Console.Clear();
            Console.Write("Enter Employee ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Employee emp = employeeService.GetEmployeeById(id);

            if (emp != null)
            {
                Console.WriteLine($"Updating Employee: {emp.FirstName} {emp.LastName}");

                Console.Write("Update First Name (leave empty to keep current): ");
                string firstNameInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstNameInput)) emp.FirstName = firstNameInput;

                Console.Write("Update Last Name (leave empty to keep current): ");
                string lastNameInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastNameInput)) emp.LastName = lastNameInput;

                Console.Write("Update Date of Birth (yyyy-MM-dd) or leave empty to keep current: ");
                string dobInput = Console.ReadLine();
                if (DateTime.TryParse(dobInput, out DateTime dateOfBirth)) emp.DateOfBirth = dateOfBirth;

                Console.Write("Update Gender (leave empty to keep current): ");
                string genderInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genderInput)) emp.Gender = genderInput;

                Console.Write("Update Email (leave empty to keep current): ");
                string emailInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(emailInput)) emp.Email = emailInput;

                Console.Write("Update Phone Number (leave empty to keep current): ");
                string phoneInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phoneInput)) emp.PhoneNumber = phoneInput;

                Console.Write("Update Address (leave empty to keep current): ");
                string addressInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(addressInput)) emp.Address = addressInput;

                Console.Write("Update Position (leave empty to keep current): ");
                string positionInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(positionInput)) emp.Position = positionInput;

                Console.Write("Update Joining Date (yyyy-MM-dd) or leave empty to keep current: ");
                string joiningInput = Console.ReadLine();
                if (DateTime.TryParse(joiningInput, out DateTime joiningDate)) emp.JoiningDate = joiningDate;

                Console.Write("Update Termination Date (yyyy-MM-dd) or leave empty to keep current: ");
                string terminationInput = Console.ReadLine();
                if (string.IsNullOrEmpty(terminationInput))
                {
                    emp.TerminationDate = null;
                }
                else if (DateTime.TryParse(terminationInput, out DateTime terminationDate))
                {
                    emp.TerminationDate = terminationDate;
                }

                employeeService.UpdateEmployee(emp);
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
            Console.ReadLine();
        }



        static void DeleteEmployee()
        {
            Console.Write("Enter Employee ID to Delete: ");
            int id;

            // Validate input
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid Employee ID.");
                return;
            }

            // Check if the employee exists before removing
            if (employeeService.EmployeeExists(id)) // Assuming you have a method to check if the employee exists
            {
                employeeService.RemoveEmployee(id);
                Console.WriteLine("Employee removed successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }

            Console.ReadLine();
        }

        #endregion
        // Payroll Processing Menu
        #region PayrollMenu
        static void PayrollProcessingMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Payroll Processing Menu:");
                Console.WriteLine("1. Add Payroll");
                Console.WriteLine("2. View Payroll by ID");
                Console.WriteLine("3. View Payrolls for Employee");
                Console.WriteLine("4. View Payrolls for Period");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddPayrollMenu();
                        break;
                    case 2:
                        ViewPayrollByIdMenu();
                        break;
                    case 3:
                        ViewPayrollsForEmployeeMenu();
                        break;
                    case 4:
                        ViewPayrollsForPeriodMenu();
                        break;
                    case 5:
                        return; // Exit to main menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddPayrollMenu()
        {
            Console.Clear();
            Console.WriteLine("Adding Payroll:");

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Pay Period Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Pay Period End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Basic Salary: ");
            decimal basicSalary = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Overtime Pay: ");
            decimal overtimePay = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Deductions: ");
            decimal deductions = decimal.Parse(Console.ReadLine());

            // Creating a new Payroll object
            Payroll payroll = new Payroll
            {
                EmployeeID = employeeId,
                PayPeriodStartDate = startDate,
                PayPeriodEndDate = endDate,
                BasicSalary = basicSalary,
                OvertimePay = overtimePay,
                Deductions = deductions,
                NetSalary = basicSalary + overtimePay - deductions // Example calculation
            };

            // Add payroll
            payrollService.AddPayroll(payroll);
            Console.WriteLine("Payroll added successfully.");
            Console.ReadLine();
        }

        static void ViewPayrollByIdMenu()
        {
            Console.Clear();
            Console.WriteLine("View Payroll by ID:");

            Console.Write("Enter Payroll ID: ");
            int payrollId = int.Parse(Console.ReadLine());

            Payroll payroll = payrollService.GetPayrollById(payrollId);
            if (payroll != null)
            {
                Console.WriteLine($"Payroll ID: {payroll.PayrollID}");
                Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                Console.WriteLine($"Pay Period: {payroll.PayPeriodStartDate:yyyy-MM-dd} to {payroll.PayPeriodEndDate:yyyy-MM-dd}");
                Console.WriteLine($"Basic Salary: {payroll.BasicSalary:C}");
                Console.WriteLine($"Overtime Pay: {payroll.OvertimePay:C}");
                Console.WriteLine($"Deductions: {payroll.Deductions:C}");
                Console.WriteLine($"Net Salary: {payroll.NetSalary:C}");
            }
            else
            {
                Console.WriteLine("Payroll not found.");
            }
            Console.ReadLine();
        }

        static void ViewPayrollsForEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("View Payrolls for Employee:");

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            List<Payroll> payrolls = payrollService.GetPayrollsForEmployee(employeeId);
            if (payrolls.Count > 0)
            {
                Console.WriteLine($"Payrolls for Employee ID {employeeId}:");
                foreach (var payroll in payrolls)
                {
                    Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Pay Period: {payroll.PayPeriodStartDate:yyyy-MM-dd} to {payroll.PayPeriodEndDate:yyyy-MM-dd}, Net Salary: {payroll.NetSalary:C}");
                }
            }
            else
            {
                Console.WriteLine("No payroll records found for this employee.");
            }
            Console.ReadLine();
        }
   
        static void ViewPayrollsForPeriodMenu()
        {
            Console.Clear();
            Console.WriteLine("View Payrolls for Period:");

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            List<Payroll> payrolls = payrollService.GetPayrollsForPeriod(startDate, endDate);
            if (payrolls.Count > 0)
            {
                Console.WriteLine($"Payrolls from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}:");
                foreach (var payroll in payrolls)
                {
                    Console.WriteLine($"Payroll ID: {payroll.PayrollID}, Employee ID: {payroll.EmployeeID}, Net Salary: {payroll.NetSalary:C}");
                }
            }
            else
            {
                Console.WriteLine("No payroll records found for this period.");
            }
            Console.ReadLine();
        }
        #endregion

        //Tax Calculation Menu
        #region TaxMenu

            static void TaxCalculationMenu()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Tax Calculation Menu:");
                    Console.WriteLine("1. Calculate Tax");
                    Console.WriteLine("2. View Tax by ID");
                    Console.WriteLine("3. View Taxes for Employee");
                    Console.WriteLine("4. View Taxes for Year");
                    Console.WriteLine("5. Back to Main Menu");
                    Console.Write("Select an option: ");

                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            CalculateTaxMenu();
                            break;
                        case 2:
                            ViewTaxByIdMenu();
                            break;
                        case 3:
                            ViewTaxesForEmployeeMenu();
                            break;
                        case 4:
                            ViewTaxesForYearMenu();
                            break;
                        case 5:
                            return; // Exit to main menu
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }

            static void CalculateTaxMenu()
            {
                Console.Clear();
                Console.WriteLine("Calculate Tax:");

                Console.Write("Enter Employee ID: ");
                int employeeId = int.Parse(Console.ReadLine());

                Console.Write("Enter Tax Year (YYYY): ");
                int taxYear = int.Parse(Console.ReadLine());

                decimal totalTax = taxService.CalculateTax(employeeId, taxYear);
                Console.WriteLine($"Total Tax for Employee {employeeId} for the year {taxYear}: {totalTax:C}");
                Console.ReadLine();
            }

            static void ViewTaxByIdMenu()
            {
                Console.Clear();
                Console.WriteLine("View Tax by ID:");

                Console.Write("Enter Tax ID: ");
                int taxId = int.Parse(Console.ReadLine());

                Tax tax = taxService.GetTaxById(taxId);
                if (tax != null)
                {
                    Console.WriteLine($"Tax ID: {tax.TaxID}");
                    Console.WriteLine($"Employee ID: {tax.EmployeeID}");
                    Console.WriteLine($"Tax Year: {tax.TaxYear}");
                    Console.WriteLine($"Taxable Income: {tax.TaxableIncome:C}");
                    Console.WriteLine($"Tax Amount: {tax.TaxAmount:C}");

                }
                else
                {
                    Console.WriteLine("Tax not found.");
                }
                Console.ReadLine();
            }

            static void ViewTaxesForEmployeeMenu()
            {
                Console.Clear();
                Console.WriteLine("View Taxes for Employee:");

                Console.Write("Enter Employee ID: ");
                int employeeId = int.Parse(Console.ReadLine());

                List<Tax> taxes = taxService.GetTaxesForEmployee(employeeId);
                if (taxes.Count > 0)
                {
                    Console.WriteLine($"Taxes for Employee ID {employeeId}:");
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxID}, Year: {tax.TaxYear}, Tax Amount: {tax.TaxAmount:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No tax records found for this employee.");
                }
                Console.ReadLine();
            }

            static void ViewTaxesForYearMenu()
            {
                Console.Clear();
                Console.WriteLine("View Taxes for Year:");

                Console.Write("Enter Tax Year (YYYY): ");
                int taxYear = int.Parse(Console.ReadLine());

                List<Tax> taxes = taxService.GetTaxesForYear(taxYear);
                if (taxes.Count > 0)
                {
                    Console.WriteLine($"Taxes for the year {taxYear}:");
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxID}, Employee ID: {tax.EmployeeID}, Tax Amount: {tax.TaxAmount:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No tax records found for this year.");
                }
                Console.ReadLine();
            }
        #endregion

        //FinanceReport Menu
        #region FinanceRecordMenu
        static void GenerateFinancialReportsMenu()
    {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Financial Record Management Menu:");
                Console.WriteLine("1. Add Financial Record");
                Console.WriteLine("2. View Financial Record by ID");
                Console.WriteLine("3. View Financial Records for Employee");
                Console.WriteLine("4. View Financial Records for Date");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddFinancialRecordMenu();
                        break;
                    case 2:
                        ViewFinancialRecordByIdMenu();
                        break;
                    case 3:
                        ViewFinancialRecordsForEmployeeMenu();
                        break;
                    case 4:
                        ViewFinancialRecordsForDateMenu();
                        break;
                    case 5:
                        return; // Exit to main menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddFinancialRecordMenu()
        {
            Console.Clear();
            Console.WriteLine("Add Financial Record:");

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Record Type: ");
            string recordType = Console.ReadLine();

            financialRecordService.AddFinancialRecord(employeeId, description, amount, recordType);
            Console.ReadLine();
        }

        static void ViewFinancialRecordByIdMenu()
        {
            Console.Clear();
            Console.WriteLine("View Financial Record by ID:");

            Console.Write("Enter Record ID: ");
            int recordId = int.Parse(Console.ReadLine());

            FinancialRecord record = financialRecordService.GetFinancialRecordById(recordId);
            if (record != null)
            {
                Console.WriteLine($"Record ID: {record.RecordID}");
                Console.WriteLine($"Employee ID: {record.EmployeeID}");
                Console.WriteLine($"Description: {record.Description}");
                Console.WriteLine($"Amount: {record.Amount:C}");
                Console.WriteLine($"Record Type: {record.RecordType}");
                Console.WriteLine($"Record Date: {record.RecordDate}");
            }
            else
            {
                Console.WriteLine("Financial record not found.");
            }
            Console.ReadLine();
        }

        static void ViewFinancialRecordsForEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("View Financial Records for Employee:");

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            List<FinancialRecord> records = financialRecordService.GetFinancialRecordsForEmployee(employeeId);
            if (records.Count > 0)
            {
                Console.WriteLine($"Financial Records for Employee ID {employeeId}:");
                foreach (var record in records)
                {
                    Console.WriteLine($"Record ID: {record.RecordID}, Description: {record.Description}, Amount: {record.Amount:C}, Record Type: {record.RecordType}, Date: {record.RecordDate}");
                }
            }
            else
            {
                Console.WriteLine("No financial records found for this employee.");
            }
            Console.ReadLine();
        }

        static void ViewFinancialRecordsForDateMenu()
        {
            Console.Clear();
            Console.WriteLine("View Financial Records for Date:");

            Console.Write("Enter Record Date (yyyy-MM-dd): ");
            DateTime recordDate = DateTime.Parse(Console.ReadLine());

            List<FinancialRecord> records = financialRecordService.GetFinancialRecordsForDate(recordDate);
            if (records.Count > 0)
            {
                Console.WriteLine($"Financial Records for Date {recordDate:yyyy-MM-dd}:");
                foreach (var record in records)
                {
                    Console.WriteLine($"Record ID: {record.RecordID}, Employee ID: {record.EmployeeID}, Description: {record.Description}, Amount: {record.Amount:C}, Record Type: {record.RecordType}");
                }
            }
            else
            {
                Console.WriteLine("No financial records found for this date.");
            }
            Console.ReadLine();
        }
        #endregion
    }
}


