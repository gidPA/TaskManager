static class TaskManager
{
    private static List<Task> TaskList = new List<Task>();

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
    public static void ViewAllTask()
    {
        int index = 0;
        Console.WriteLine("{0, -9} | {1,-40} | {2,-30} | {3,-20}", "Task ID", "Task Description", "Due Date", "Status");
        Console.WriteLine(new string('-', 99)); // separator line

        foreach(Task task in TaskList.OrderBy(t => t.DueDate)){
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
            Console.WriteLine("\tStatus: \t\t{0}", TaskStatusNames.GetTaskName(task.Status));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Multiple tasks with ID {0} were found", taskId);
        }
    }

    public static int MarkTaskAsDone(int taskId){
        try{
            Task? task = TaskList.SingleOrDefault(t => t.TaskId == taskId);

            if (task is null){
                return -1;
            }
            else if (task.Status == TaskStatus.Done){
                return 0;
            } else {
                task.Status = TaskStatus.Done;
                return 1;
            }
        } catch (InvalidOperationException){
            return -1;
        } catch (NullReferenceException){
            return -1;
        }
    }

}