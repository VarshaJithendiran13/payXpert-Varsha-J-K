using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PayrollSystem.Model;
using PayrollSystem.Utilities;
using PayrollSystem.Repository;
using PayrollSystem.Services;

namespace PayrollSystem.Repository
{
    internal class FinancialRecordRepository : IFinancialRecordRepository
    {
        SqlConnection sqlConnection;
        SqlCommand cmd;

        public FinancialRecordRepository()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            cmd = new SqlCommand();
        }

        public int AddFinancialRecord(FinancialRecord record)
        {
            try
            {
                cmd.CommandText = "INSERT INTO FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType) " +
                                  "VALUES (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
                cmd.Parameters.AddWithValue("@RecordDate", record.RecordDate);
                cmd.Parameters.AddWithValue("@Description", record.Description);
                cmd.Parameters.AddWithValue("@Amount", record.Amount);
                cmd.Parameters.AddWithValue("@RecordType", record.RecordType);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding financial record. See inner exception for details.", sqlEx);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            try
            {
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE RecordID = @RecordID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RecordID", recordId);
                cmd.Connection = sqlConnection;

                FinancialRecord record = null;

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        record = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = (string)reader["Description"],
                            Amount = (decimal)reader["Amount"],
                            RecordType = (string)reader["RecordType"]
                        };
                    }
                }
                return record;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving financial record. See inner exception for details.", sqlEx);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            try
            {
                List<FinancialRecord> records = new List<FinancialRecord>();
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = (string)reader["Description"],
                            Amount = (decimal)reader["Amount"],
                            RecordType = (string)reader["RecordType"]
                        };
                        records.Add(record);
                    }
                }
                return records;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving financial records for the employee. See inner exception for details.", sqlEx);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            try
            {
                List<FinancialRecord> records = new List<FinancialRecord>();
                cmd.CommandText = "SELECT * FROM FinancialRecord WHERE CAST(RecordDate AS DATE) = @RecordDate";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RecordDate", recordDate.Date);
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FinancialRecord record = new FinancialRecord
                        {
                            RecordID = (int)reader["RecordID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            RecordDate = (DateTime)reader["RecordDate"],
                            Description = (string)reader["Description"],
                            Amount = (decimal)reader["Amount"],
                            RecordType = (string)reader["RecordType"]
                        };
                        records.Add(record);
                    }
                }
                return records;
            }
            catch (SqlException sqlEx)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving financial records for the specified date. See inner exception for details.", sqlEx);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
