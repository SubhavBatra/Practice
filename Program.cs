using System;
using System.Collections.Generic;
using System.IO;

class TaskItem
{
    public string Title;
    public int Priority;
    public bool Done;

    public TaskItem(string title, int priority, bool done)
    {
        Title = title;
        Priority = priority;
        Done = done;
    }

    public string ToFileLine()
    {
        // serialize to string for saving
        return Title + "|" + Priority + "|" + Done;
    }

    public static TaskItem FromFileLine(string line)
    {
        // parse line back into TaskItem
        string[] parts = line.Split('|');
        // Basic defensive parsing
        string title = parts.Length > 0 ? parts[0] : "";
        int prio = 0;
        bool dn = false;

        if (parts.Length > 1)
        {
            int.TryParse(parts[1], out prio);
        }
        if (parts.Length > 2)
        {
            bool.TryParse(parts[2], out dn);
        }

        return new TaskItem(title, prio, dn);
    }
}

class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>();
    private string fileName;

    public TaskManager(string fileName)
    {
        this.fileName = fileName;
        LoadFromFile();
    }

    public void AddTask(string title, int priority)
    {
        tasks.Add(new TaskItem(title, priority, false));
    }

    public void ListTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks.\n");
            return;
        }

        Console.WriteLine("Idx | Done | Priority | Title");
        Console.WriteLine("---------------------------------");
        for (int i = 0; i < tasks.Count; i++)
        {
            TaskItem t = tasks[i];
            Console.WriteLine($"{i}   | {(t.Done ? "YES " : "NO  ")} | {t.Priority}        | {t.Title}");
        }
        Console.WriteLine();
    }

    public void MarkDone(int index)
    {
        if (index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid index.\n");
            return;
        }
        tasks[index].Done = true;
        Console.WriteLine("Task marked complete.\n");
    }

    public void SaveToFile()
    {
        List<string> lines = new List<string>();
        // header (optional)
        lines.Add("Title|Priority|Done");
        for (int i = 0; i < tasks.Count; i++)
        {
            lines.Add(tasks[i].ToFileLine());
        }
        File.WriteAllLines(fileName, lines.ToArray());
    }

    private void LoadFromFile()
    {
        if (!File.Exists(fileName))
        {
            return;
        }

        string[] lines = File.ReadAllLines(fileName);
        // skip header if present
        int startIndex = 0;
        if (lines.Length > 0 && lines[0].StartsWith("Title|"))
        {
            startIndex = 1;
        }

        for (int i = startIndex; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (line.Length == 0) continue;

            TaskItem task = TaskItem.FromFileLine(line);
            tasks.Add(task);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskManager manager = new TaskManager("tasks.csv");

        while (true)
        {
            Console.WriteLine("=== TASK MANAGER ===");
            Console.WriteLine("1) Add Task");
            Console.WriteLine("2) List Tasks");
            Console.WriteLine("3) Mark Task Complete");
            Console.WriteLine("4) Save and Exit");
            Console.Write("Choose: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                Console.Write("Task title: ");
                string? title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Title cannot be empty.\n");
                    continue;
                }

                Console.Write("Priority (number): ");
                string? prioStr = Console.ReadLine();
                int prio;
                if (!int.TryParse(prioStr, out prio))
                {
                    Console.WriteLine("Priority must be a number.\n");
                    continue;
                }

                manager.AddTask(title.Trim(), prio);
                Console.WriteLine("Task added.\n");
            }
            else if (choice == "2")
            {
                manager.ListTasks();
            }
            else if (choice == "3")
            {
                Console.Write("Which task index to mark done? ");
                string? idxStr = Console.ReadLine();
                int idx;
                if (!int.TryParse(idxStr, out idx))
                {
                    Console.WriteLine("Not a valid index.\n");
                    continue;
                }
                manager.MarkDone(idx);
            }
            else if (choice == "4")
            {
                manager.SaveToFile();
                Console.WriteLine("Saved. Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice.\n");
            }
        }
    }
}
