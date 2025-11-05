using System;
using System.Collections.Generic;

namespace StudentApp;

public class ClassStats
{
    public int Count { get; init; }
    public double Average { get; init; }
    public string MaxName { get; init; } = "";
    public int MaxMark { get; init; }
    public string MinName { get; init; } = "";
    public int MinMark { get; init; }
}

public class StudentService
{
    private readonly List<Student> _students = new();

    public IReadOnlyList<Student> All => _students;

    public bool Exists(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;
        string needle = name.Trim().ToLower();
        for (int i = 0; i < _students.Count; i++)
        {
            if ((_students[i].Name ?? "").Trim().ToLower() == needle)
                return true;
        }
        return false;
    }

    public void Add(string name, int mark)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (mark < 0 || mark > 100)
            throw new ArgumentOutOfRangeException(nameof(mark), "Mark must be 0..100.");
        if (Exists(name))
            throw new InvalidOperationException("Duplicate student name.");

        _students.Add(new Student(name.Trim(), mark));
    }

    public ClassStats ComputeStats()
    {
        if (_students.Count == 0)
            throw new InvalidOperationException("No students to compute stats.");

        int total = 0;
        int max = int.MinValue, min = int.MaxValue;
        string maxName = "", minName = "";

        for (int i = 0; i < _students.Count; i++)
        {
            int m = _students[i].Mark;
            total += m;
            if (m > max) { max = m; maxName = _students[i].Name; }
            if (m < min) { min = m; minName = _students[i].Name; }
        }

        return new ClassStats
        {
            Count = _students.Count,
            Average = (double)total / _students.Count,
            MaxName = maxName,
            MaxMark = max,
            MinName = minName,
            MinMark = min
        };
    }

    public List<Student> SearchByName(string query)
    {
        var result = new List<Student>();
        string needle = (query ?? "").Trim().ToLower();
        for (int i = 0; i < _students.Count; i++)
        {
            string hay = (_students[i].Name ?? "").Trim().ToLower();
            if (hay.Contains(needle)) result.Add(_students[i]);
        }
        return result;
    }
}
