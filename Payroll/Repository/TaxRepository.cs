using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PayrollSystem.Exceptions;
using PayrollSystem.Model;
using PayrollSystem.Utilities;

namespace PayrollSystem.Repository
{
    public class TaxRepository : ITaxRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlCommand _cmd;

        public TaxRepository()
        {
            _sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        // Method to add a tax record
        public int AddTax(Tax tax)
        {
            try
            {
                _cmd.CommandText = "INSERT INTO Tax (EmployeeID, TaxYear, TaxableIncome, TaxAmount) " +
                                   "VALUES (@EmployeeID, @TaxYear, @TaxableIncome, @TaxAmount)";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@EmployeeID", tax.EmployeeID);
                _cmd.Parameters.AddWithValue("@TaxYear", tax.TaxYear);
                _cmd.Parameters.AddWithValue("@TaxableIncome", tax.TaxableIncome);
                _cmd.Parameters.AddWithValue("@TaxAmount", tax.TaxAmount);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                int rowsAffected = _cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding tax record. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            // Input validation
            if (employeeId <= 0)
            {
                throw new InvalidInputException("Employee ID must be greater than zero.");
            }
            if (taxYear < 2000 || taxYear > DateTime.Now.Year)
            {
                throw new TaxCalculationException("Tax Year must be in the range 2000 and the current year.");
            }

            decimal taxRate = 0.1m;
            decimal totalTaxableIncome = 0;

            try
            {
                // Calculate total taxable income
                using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT SUM(BasicSalary + OvertimePay - Deductions) AS TotalTaxableIncome FROM Payroll WHERE EmployeeID = @EmployeeID AND YEAR(PayPeriodStartDate) = @TaxYear";
                        cmd.Connection = sqlConnection;
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                        cmd.Parameters.AddWithValue("@TaxYear", taxYear);

                        sqlConnection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalTaxableIncome = reader["TotalTaxableIncome"] != DBNull.Value ? (decimal)reader["TotalTaxableIncome"] : 0;
                            }
                        }
                    }
                }

                // Return early if no taxable income
                if (totalTaxableIncome == 0)
                {
                    return 0;
                }

                // Calculate tax amount
                decimal taxAmount = totalTaxableIncome * taxRate;

                // Insert tax details into the database
                using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO Tax (EmployeeID, TaxYear, TaxableIncome, TaxAmount) VALUES (@EmployeeID, @TaxYear, @TaxableIncome, @TaxAmount)";
                        cmd.Connection = sqlConnection;
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                        cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                        cmd.Parameters.AddWithValue("@TaxableIncome", totalTaxableIncome);
                        cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);

                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return taxAmount;
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("An error occurred while calculating the tax.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while calculating the tax.", ex);
            }
        }


        // Method to get a tax record by its ID
        public Tax GetTaxById(int taxId)
        {
            try
            {
                _cmd.CommandText = "SELECT * FROM Tax WHERE TaxID = @TaxID";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@TaxID", taxId);
                _cmd.Connection = _sqlConnection;

                Tax tax = null;
                _sqlConnection.Open();

                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tax = new Tax
                        {
                            TaxID = (int)reader["TaxID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            TaxYear = (int)reader["TaxYear"],
                            TaxableIncome = (decimal)reader["TaxableIncome"],
                            TaxAmount = (decimal)reader["TaxAmount"]
                        };
                    }
                }
                return tax;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving tax record. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        // Method to get all tax records for a specific employee
        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            try
            {
                List<Tax> taxes = new List<Tax>();
                _cmd.CommandText = "SELECT * FROM Tax WHERE EmployeeID = @EmployeeID";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tax tax = new Tax
                        {
                            TaxID = (int)reader["TaxID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            TaxYear = (int)reader["TaxYear"],
                            TaxableIncome = (decimal)reader["TaxableIncome"],
                            TaxAmount = (decimal)reader["TaxAmount"]
                        };
                        taxes.Add(tax);
                    }
                }
                return taxes;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving tax records for the employee. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        // Method to get all tax records for a specific year
        public List<Tax> GetTaxesForYear(int taxYear)
        {
            try
            {
                List<Tax> taxes = new List<Tax>();
                _cmd.CommandText = "SELECT * FROM Tax WHERE TaxYear = @TaxYear";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@TaxYear", taxYear);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Check if the columns exist in the reader before accessing them
                        Tax tax = new Tax
                        {
                            TaxID = reader["TaxID"] != DBNull.Value ? (int)reader["TaxID"] : 0,
                            EmployeeID = reader["EmployeeID"] != DBNull.Value ? (int)reader["EmployeeID"] : 0,
                            TaxYear = reader["TaxYear"] != DBNull.Value ? (int)reader["TaxYear"] : 0,
                            TaxableIncome = reader["TaxableIncome"] != DBNull.Value ? (decimal)reader["TaxableIncome"] : 0m,
                            TaxAmount = reader["TaxAmount"] != DBNull.Value ? (decimal)reader["TaxAmount"] : 0m
                        };
                        taxes.Add(tax);
                    }
                }
                return taxes;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving tax records for the specified year. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
