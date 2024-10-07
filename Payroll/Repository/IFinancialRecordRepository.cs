using PayrollSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollSystem.Services;
using PayrollSystem.Repository;

namespace PayrollSystem.Repository
{
    public interface IFinancialRecordRepository
    {
        int AddFinancialRecord(FinancialRecord record);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}

