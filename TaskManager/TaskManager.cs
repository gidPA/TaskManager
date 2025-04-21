static class TaskManager
{
    private static List<Task> TaskList = new List<Task>();

    // private static string TruncateString(string text, int maxLength)
    // {
    //     if (text.Length <= maxLength) return text;
    //     return text.Substring(0, maxLength - 3) + "..."; // add ellipsis
    // }

    public static string TruncateString(this string text, int maxLength)
    {
        if (text.Length <= maxLength) return text;
        return text.Substring(0, maxLength - 3) + "..."; // add ellipsis
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
        TaskList.Add(new Task(description, new DateTime(year, month, day, hour, minute, 0)));
    }
    public static void ViewAllTask()
    {
        int index = 0;
        Console.WriteLine("{0, -9} | {1,-40} | {2,-30} | {3,-20}", "Task ID", "Task Description", "Due Date", "Status");
        Console.WriteLine(new string('-', 99)); // separator line

        foreach(Task task in TaskList.OrderBy(t => t.DueDate)){
            Console.WriteLine("{0, -9} | {1,-40} | {2,-30} | {3,-20}", index, task.Description.TruncateString(40), task.DueDate.ToString("yyyy-MM-dd HH:mm"), task.GetTaskName(task.Status));
            index += 1;
        }
    }

    public static void ViewTaskByID(int taskID){
        Console.WriteLine("Task Details");
        Console.WriteLine("\tDescription: \t{0}", TaskList[taskID].Description);
        Console.WriteLine("\tDue date: \t{0}", TaskList[0].DueDate.ToString("yyyy-mm-dd hh:mm"));
        Console.WriteLine("\tStatus: \t\t{0}", TaskList[0].GetTaskName(TaskList[0].Status));
    }

    public static int MarkTaskAsDone(int taskID){
        try{
            if (TaskList[taskID].Status == TaskStatus.Done){
                return 0;
            } else {
                TaskList[taskID].Status = TaskStatus.Done;
                return 1;
            }
        } catch (ArgumentOutOfRangeException){
            return -1;
        }
    }

}