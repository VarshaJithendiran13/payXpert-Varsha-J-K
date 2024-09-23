create database payXpert;
USE payXpert;

-- Employee Table
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50),
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(15) NOT NULL,
    Res_Address VARCHAR(255),
    Position VARCHAR(50) NOT NULL,
    JoiningDate DATE NOT NULL,
    TerminationDate DATE
);

-- Payroll Table
CREATE TABLE Payroll (
    PayrollID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    PayPeriodStartDate DATE NOT NULL,
    PayPeriodEndDate DATE,
    BasicSalary DECIMAL(10, 2) NOT NULL,
    OvertimePay DECIMAL(10, 2) NOT NULL,
    Deductions DECIMAL(10, 2) NOT NULL,
    NetSalary DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

-- Tax Table
CREATE TABLE Tax (
    TaxID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    TaxYear INT NOT NULL,
    TaxableIncome DECIMAL(10, 2) NOT NULL,
    TaxAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);

-- FinancialRecord Table
CREATE TABLE FinancialRecord (
    RecordID INT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    RecordDate DATE NOT NULL,
    Category VARCHAR(255),
    Amount DECIMAL(10, 2) NOT NULL,
    RecordType VARCHAR(50) NOT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ON DELETE CASCADE
);
