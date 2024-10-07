using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Model;
using PayrollSystem.Services.Interfaces;
using PayrollSystem.Exceptions;
using PayrollSystem.Repository;

namespace PayrollSystem.Services
{
    public class FinancialRecordService : IFinancialRecordService
    {
        private readonly IFinancialRecordRepository _financialRecordRepository;

        // Constructor
        public FinancialRecordService(IFinancialRecordRepository financialRecordRepository)
        {
            _financialRecordRepository = financialRecordRepository;
        }
        public FinancialRecordService()
        {
            _financialRecordRepository = new FinancialRecordRepository();
        }

        public void AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            FinancialRecord record = new FinancialRecord
            {
                EmployeeID = employeeId,
                Description = description,
                Amount = amount,
                RecordType = recordType,
                RecordDate = DateTime.Now // Set the record date to now, or you can customize this
            };

            int addRecordStatus = _financialRecordRepository.AddFinancialRecord(record);
            if (addRecordStatus > 0)
            {
                Console.WriteLine("Financial record added successfully.");
            }
            else
            {
                Console.WriteLine("Addition failed.");
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            return _financialRecordRepository.GetFinancialRecordById(recordId);
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            return _financialRecordRepository.GetFinancialRecordsForEmployee(employeeId);
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            return _financialRecordRepository.GetFinancialRecordsForDate(recordDate);
        }
    }
}
