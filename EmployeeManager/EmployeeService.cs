// EmployeeService.cs
using System;
using System.Collections.Generic;

namespace EmployeeManager;

public class EmployeeService
{
    private readonly List<Employee> _employees = new();
    public IReadOnlyList<Employee> All => _employees; // safe exposure

    public void Add(string name, decimal salary)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (salary <= 0)
            throw new ArgumentOutOfRangeException(nameof(salary), "Salary must be > 0.");
        if (Exists(name))
            throw new InvalidOperationException("Duplicate employee name.");

        _employees.Add(new Employee(name.Trim(), salary));
    }

    public void GiveRaise(string name, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Raise must be > 0.");

        var emp = FindByName(name);
        if (emp == null)
            throw new KeyNotFoundException("Employee not found.");

        emp.IncreaseSalary(amount);
    }

    public decimal TotalSalary()
    {
        decimal sum = 0m;
        for (int i = 0; i < _employees.Count; i++)
            sum += _employees[i].Salary;
        return sum;
    }

    public List<Employee> Search(string query)
    {
        var result = new List<Employee>();
        string needle = (query ?? "").Trim();

        for (int i = 0; i < _employees.Count; i++)
        {
            if (_employees[i].Name.Contains(needle, StringComparison.OrdinalIgnoreCase))
                result.Add(_employees[i]);
        }
        return result;
    }

    // —— helpers ——

    private bool Exists(string name)
        => FindByName(name) != null;

    private Employee? FindByName(string name)
    {
        for (int i = 0; i < _employees.Count; i++)
        {
            if (string.Equals(_employees[i].Name, name?.Trim(),
                StringComparison.OrdinalIgnoreCase))
                return _employees[i];
        }
        return null;
    }
}
