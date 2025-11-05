using System;
using StudentApp;

class Program
{
    static void Main()
    {
        var svc = new StudentService();

        while (true)
        {
            Console.WriteLine("=== Student Record Manager ===");
            Console.WriteLine("1) Add Student");
            Console.WriteLine("2) View All");
            Console.WriteLine("3) Show Stats");
            Console.WriteLine("4) Search by Name");
            Console.WriteLine("5) Exit");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine() ?? "";

            try
            {
                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Mark (0-100): ");
                    string mstr = Console.ReadLine() ?? "";
                    if (!int.TryParse(mstr, out int mark)) { Console.WriteLine("Invalid mark.\n"); continue; }

                    svc.Add(name, mark);
                    Console.WriteLine("Added.\n");
                }
                else if (choice == "2")
                {
                    if (svc.All.Count == 0) { Console.WriteLine("No records.\n"); continue; }
                    Console.WriteLine("Name\tMark");
                    Console.WriteLine("--------------");
                    foreach (var s in svc.All) Console.WriteLine($"{s.Name}\t{s.Mark}");
                    Console.WriteLine();
                }
                else if (choice == "3")
                {
                    var stats = svc.ComputeStats();
                    Console.WriteLine($"Count: {stats.Count}");
                    Console.WriteLine($"Average: {stats.Average:0.00}");
                    Console.WriteLine($"Highest: {stats.MaxName} ({stats.MaxMark})");
                    Console.WriteLine($"Lowest:  {stats.MinName} ({stats.MinMark})\n");
                }
                else if (choice == "4")
                {
                    Console.Write("Search text: ");
                    string q = Console.ReadLine() ?? "";
                    var results = svc.SearchByName(q);
                    if (results.Count == 0) { Console.WriteLine("(no matches)\n"); continue; }
                    Console.WriteLine("Name\tMark");
                    Console.WriteLine("--------------");
                    foreach (var s in results) Console.WriteLine($"{s.Name}\t{s.Mark}");
                    Console.WriteLine();
                }
                else if (choice == "5")
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
                // Show friendly message (don’t crash)
                Console.WriteLine("Error: " + ex.Message + "\n");
            }
        }
    }
}
