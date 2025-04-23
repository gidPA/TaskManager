static class TaskManager
{
    private static List<Task> TaskList = new List<Task>();

    static TaskManager()
    {
        var csvReader = new CSVReader("TaskList.csv", new string[]{"TaskId", "Description", "DueDate","Status"});
        var initialTaskData = csvReader.ReadCSVFile();
        
        foreach(var task in initialTaskData){
            TaskList.Add
            (
                new Task
                (
                    task["Description"],                                                    // Description
                    DateTime.ParseExact(task["DueDate"], "dd-MM-yyyy HH:mm", null),         // Due Date
                    int.Parse(task["TaskId"]),                                              // Task ID
                    task["Status"] == "Done" ? TaskStatus.Done : TaskStatus.InProgress      // Task Status
                )
            );
        }


    }

    public static string TruncateString(this string text, int maxLength)
    {
        if (text.Length <= maxLength) return text;
        return text.Substring(0, maxLength - 3) + "..."; // add ellipsis
    }
    private static int GetNewID()
    {
        return TaskList.Any() ? TaskList.Max(t => t.TaskId) + 1 : 1;
    }

    public static void AddNewTask(
        string description,
        int year,
        int month,
        int day,
        int hour,
        int minute
    )
    {
        TaskList.Add(new Task(description, new DateTime(year, month, day, hour, minute, 0), GetNewID()));
    }

    public static void AddNewTask(string description, DateTime dueDate)
    {
        TaskList.Add(new Task(description, dueDate, GetNewID()));
    }
    public static void ViewAllTask()
    {
        int index = 0;
        Console.WriteLine("{0, -9} | {1,-40} | {2,-30} | {3,-20}", "Task ID", "Task Description", "Due Date", "Status");
        Console.WriteLine(new string('-', 99)); // separator line

        foreach (Task task in TaskList.OrderBy(t => t.DueDate))
        {
            Console.WriteLine
            (
                "{0, -9} | {1,-40} | {2,-30} | {3,-20}",
                task.TaskId, task.Description.TruncateString(40),
                task.DueDate.ToString("yyyy-MM-dd HH:mm"),
                TaskStatusNames.GetTaskName(task.Status)
            );
            index += 1;
        }
    }

    public static void ViewTaskByID(int taskId)
    {
        try
        {
            Task? task = TaskList.SingleOrDefault(t => t.TaskId == taskId);

            if (task is null)
            {
                Console.WriteLine("No task with ID {0} was found", taskId);
                return;
            }

            Console.WriteLine("Task Details");
            Console.WriteLine("\tDescription: \t{0}", task.Description);
            Console.WriteLine("\tDue date: \t{0}", task.DueDate.ToString("yyyy-MM-dd HH:mm"));
            Console.WriteLine("\tStatus: \t{0}", TaskStatusNames.GetTaskName(task.Status));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Multiple tasks with ID {0} were found", taskId);
        }
    }

    public static int MarkTaskAsDone(int taskId)
    {
        try
        {
            Task? task = TaskList.SingleOrDefault(t => t.TaskId == taskId);

            if (task is null)
            {
                return -1;
            }
            else if (task.Status == TaskStatus.Done)
            {
                return 0;
            }
            else
            {
                task.Status = TaskStatus.Done;
                return 1;
            }
        }
        catch (InvalidOperationException)
        {
            return -1;
        }
        catch (NullReferenceException)
        {
            return -1;
        }
    }

    public static List<Task> FilterTask(string keyword)
    {
        int idFilter = 0;
        int.TryParse(keyword, out idFilter);

        string filter = keyword.ToLower();

        if (filter == "in progress" || filter == "done")
        {
            TaskStatus availabilityFilter = filter == "in progress" ? TaskStatus.InProgress : TaskStatus.Done;
            return TaskList
                .Where(t =>
                    t.Description.ToLower().Contains(filter) ||
                    t.Status.Equals(availabilityFilter) ||
                    t.TaskId.Equals(idFilter))
                .ToList();
        }

        return TaskList
            .Where(t =>
                t.Description.ToLower().Contains(filter) ||
                t.TaskId.Equals(idFilter))
            .ToList();
    }

    public static void DisplayFilterResult(string keyword)
    {
        var taskList = FilterTask(keyword);
        if (!taskList.Any() || taskList is null)
        {
            Console.WriteLine("No results");
            return;
        }
        else
        {
            Console.WriteLine("Found {0} entries", taskList.Count);
            taskList.OrderBy(t => t.DueDate);
            foreach (Task task in taskList)
            {
                ViewTaskByID(task.TaskId);
            }

        }
    }

}