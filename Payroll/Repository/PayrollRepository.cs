using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;
using PayrollSystem.Repository;
using PayrollSystem.Services;
using System.Data.SqlClient;
using PayrollSystem.Utilities;

namespace PayrollSystem.Repository
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlCommand _cmd;

        public PayrollRepository()
        {
            _sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            _cmd = new SqlCommand();
        }

        public int AddPayroll(Payroll payroll)
        {
            try
            {
                _cmd.CommandText = "INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary) " +
                                   "VALUES (@EmployeeID, @PayPeriodStartDate, @PayPeriodEndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary)";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@EmployeeID", payroll.EmployeeID);
                _cmd.Parameters.AddWithValue("@PayPeriodStartDate", payroll.PayPeriodStartDate);
                _cmd.Parameters.AddWithValue("@PayPeriodEndDate", payroll.PayPeriodEndDate);
                _cmd.Parameters.AddWithValue("@BasicSalary", payroll.BasicSalary);
                _cmd.Parameters.AddWithValue("@OvertimePay", payroll.OvertimePay);
                _cmd.Parameters.AddWithValue("@Deductions", payroll.Deductions);
                _cmd.Parameters.AddWithValue("@NetSalary", payroll.NetSalary);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                int rowsAffected = _cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding payroll. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public Payroll GetPayrollById(int payrollId)
        {
            try
            {
                _cmd.CommandText = "SELECT * FROM Payroll WHERE PayrollID = @PayrollID";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@PayrollID", payrollId);
                _cmd.Connection = _sqlConnection;

                Payroll payroll = null;
                _sqlConnection.Open();

                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (decimal)reader["BasicSalary"],
                            OvertimePay = (decimal)reader["OvertimePay"],
                            Deductions = (decimal)reader["Deductions"],
                            NetSalary = (decimal)reader["NetSalary"]
                        };
                    }
                }
                return payroll;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving payroll. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId));

            List<Payroll> payrolls = new List<Payroll>();
            _cmd.CommandText = "SELECT * FROM Payroll WHERE EmployeeID = @EmployeeID";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            _cmd.Connection = _sqlConnection;

            _sqlConnection.Open();
            using (SqlDataReader reader = _cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                        PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                        BasicSalary = (decimal)reader["BasicSalary"],
                        OvertimePay = (decimal)reader["OvertimePay"],
                        Deductions = (decimal)reader["Deductions"],
                        NetSalary = (decimal)reader["NetSalary"]
                    };
                    payrolls.Add(payroll);
                }
            }

            return payrolls; 
        }


        public List<Payroll> GetPayrollsForPeriod(DateTime payPeriodStartDate, DateTime payPeriodEndDate)
        {
            try
            {
                List<Payroll> payrolls = new List<Payroll>();
                _cmd.CommandText = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @payPeriodStartDate AND PayPeriodEndDate <= @payPeriodEndDate";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@payPeriodStartDate", payPeriodStartDate);
                _cmd.Parameters.AddWithValue("@payPeriodEndDate", payPeriodStartDate);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (decimal)reader["BasicSalary"],
                            OvertimePay = (decimal)reader["OvertimePay"],
                            Deductions = (decimal)reader["Deductions"],
                            NetSalary = (decimal)reader["NetSalary"]
                        };
                        payrolls.Add(payroll);
                    }
                }
                return payrolls;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving payrolls for the period. See inner exception for details.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        public List<Payroll> GetPayrollsForEmployeeInYear(int employeeId, int year)
        {
            List<Payroll> payrolls = new List<Payroll>();
            try
            {
                // SQL query updated to filter by year
                _cmd.CommandText = @"
                SELECT * 
                FROM Payroll 
                WHERE EmployeeID = @EmployeeID 
                  AND (YEAR(PayPeriodStartDate) = @Year OR YEAR(PayPeriodEndDate) = @Year)";

                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                _cmd.Parameters.AddWithValue("@Year", year);
                _cmd.Connection = _sqlConnection;

                _sqlConnection.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payroll payroll = new Payroll
                        {
                            PayrollID = (int)reader["PayrollID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                            PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                            BasicSalary = (decimal)reader["BasicSalary"],
                            OvertimePay = (decimal)reader["OvertimePay"],
                            Deductions = (decimal)reader["Deductions"],
                            NetSalary = (decimal)reader["NetSalary"]
                        };
                        payrolls.Add(payroll);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving payrolls for the employee.", sqlEx);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return payrolls;
        }
    }
}
