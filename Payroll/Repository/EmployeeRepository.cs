using PayrollSystem.Model;
using PayrollSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Utilities;
using PayrollSystem.Exceptions;

namespace PayrollSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        SqlConnection sqlConnection;
        SqlCommand cmd;

        public EmployeeRepository()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            cmd = new SqlCommand();
        }

        public int AddEmployee(Employee employee)
        {
            try
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.Append("INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) ");
                sqlCommand.Append("VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)");
                cmd.CommandText = sqlCommand.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);

                // Check if TerminationDate is null and add accordingly
                if (employee.TerminationDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value); // Use DBNull for SQL null
                }

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error connecting to the database while adding an employee.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException($"An error occurred while adding the employee: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Employee GetEmployeeById(int employeeId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Connection = sqlConnection;

                Employee employee = null;

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Gender = (string)reader["Gender"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Address = (string)reader["Address"],
                            Position = (string)reader["Position"],
                            JoiningDate = (DateTime)reader["JoiningDate"],
                            TerminationDate = reader["TerminationDate"] as DateTime?
                        };
                    }
                    else
                    {
                        throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                    }
                }

                return employee;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error connecting to the database while retrieving employee.", ex);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException($"An error occurred while retrieving the employee: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                cmd.CommandText = "SELECT * FROM Employee";
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Gender = (string)reader["Gender"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Address = (string)reader["Address"],
                            Position = (string)reader["Position"],
                            JoiningDate = (DateTime)reader["JoiningDate"],
                            TerminationDate = reader["TerminationDate"] as DateTime?
                        };
                        employees.Add(employee);
                    }
                }
                return employees;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error connecting to the database while retrieving all employees.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException($"An error occurred while retrieving employees: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                StringBuilder sqlCommand = new StringBuilder();

                sqlCommand.Append("UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, ");
                sqlCommand.Append("Gender = @Gender, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, ");
                sqlCommand.Append("Position = @Position, JoiningDate = @JoiningDate, TerminationDate = @TerminationDate ");
                sqlCommand.Append("WHERE EmployeeID = @EmployeeID");

                cmd.CommandText = sqlCommand.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);

                // Check if TerminationDate is null and add accordingly
                if (employee.TerminationDate.HasValue)
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TerminationDate", DBNull.Value); // Use DBNull for SQL null
                }

                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error connecting to the database while updating employee.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException($"An error occurred while updating the employee: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool RemoveEmployee(int employeeId)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; // Return true if a row was deleted, false otherwise
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error connecting to the database while deleting employee.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException($"An error occurred while deleting the employee: {ex.Message}", ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}

