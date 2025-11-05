// Program.cs
using System;
using EmployeeManager;

class Program
{
    static void Main()
    {
        var svc = new EmployeeService();

        while (true)
        {
            Console.WriteLine("=== Employee Salary Manager ===");
            Console.WriteLine("1) Add Employee");
            Console.WriteLine("2) Show All");
            Console.WriteLine("3) Give Raise");
            Console.WriteLine("4) Show Total Salary Expense");
            Console.WriteLine("5) Search by Name");
            Console.WriteLine("6) Exit");
            Console.Write("Choice: ");
            string choice = Console.ReadLine() ?? "";

            try
            {
                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Salary (> 0): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
                    {
                        Console.WriteLine("Invalid salary.\n");
                        continue;
                    }

                    svc.Add(name, salary);
                    Console.WriteLine("Employee added.\n");
                }
                else if (choice == "2")
                {
                    var all = svc.All;
                    if (all.Count == 0) { Console.WriteLine("No employees.\n"); continue; }

                    Console.WriteLine("Name\t\tSalary");
                    Console.WriteLine("---------------------------");
                    foreach (var e in all)
                        Console.WriteLine($"{e.Name}\t\t{e.Salary:0.00}");
                    Console.WriteLine();
                }
                else if (choice == "3")
                {
                    Console.Write("Employee name: ");
                    string name = Console.ReadLine() ?? "";

                    Console.Write("Raise amount (> 0): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal raise))
                    {
                        Console.WriteLine("Invalid amount.\n");
                        continue;
                    }

                    svc.GiveRaise(name, raise);
                    Console.WriteLine("Raise applied.\n");
                }
                else if (choice == "4")
                {
                    decimal total = svc.TotalSalary();
                    Console.WriteLine($"Total salary expense: {total:0.00}\n");
                }
                else if (choice == "5")
                {
                    Console.Write("Search text: ");
                    string q = Console.ReadLine() ?? "";

                    var matches = svc.Search(q);
                    if (matches.Count == 0) { Console.WriteLine("(no matches)\n"); continue; }

                    Console.WriteLine("Name\t\tSalary");
                    Console.WriteLine("---------------------------");
                    foreach (var e in matches)
                        Console.WriteLine($"{e.Name}\t\t{e.Salary:0.00}");
                    Console.WriteLine();
                }
                else if (choice == "6")
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.\n");
                }
            }
            catch (Exception ex)
            {
                // Turn rule violations into friendly messages
                Console.WriteLine("Error: " + ex.Message + "\n");
            }
        }
    }
}
