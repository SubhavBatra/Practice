// Employee.cs
using System;

namespace EmployeeManager;

public class Employee
{
    public string Name { get; }
    public decimal Salary { get; private set; }

    public Employee(string name, decimal salary)
    {
        Name = (name ?? "").Trim();
        Salary = salary;
    }

    public void IncreaseSalary(decimal amount)
    {
        // Caller (service) validates amount > 0
        Salary += amount;
    }
}
