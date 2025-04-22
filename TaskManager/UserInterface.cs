static class TaskManagerUI
{
    public static void InitialUI()
    {
        Console.WriteLine("Welcome to GPA's Task Manager\n");
        DisplayOptions();
    }
    public static void MainLoop()
    {
        while (true)
        {
            string? option;

            Console.Write("Choose your option: ");

            option = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(option))
            {
                Console.WriteLine("Please enter a valid option");
                continue;
            }

            //TODO: jadikan enum
            //TODO: Resolve kasus kalau enter, program langsung keluar -> Solved
            //TODO: Ada OutOfRangeException saat ID di luar batas
            switch (option)
            {
                case "1":
                    DisplayOptions();
                    break;
                case "2":
                    CreateNewTask();
                    break;
                case "3":
                    ViewAllTask();
                    break;
                case "4":
                    SearchTask();
                    break;
                case "5":
                    MarkTaskAsDone();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Please Enter a valid option");
                    break;
            }
        }

    }

    static void DisplayOptions()
    {
        Console.WriteLine
        (
            "Welcome to GPA's Task Manager\n" +
            "Select an action:\n" +
            "\t1. Display this action list\n" +
            "\t2. Create New Task\n" +
            "\t3. View All Task\n" +
            "\t4. Search Tasks\n" +
            "\t5. Mark Task as complete\n" +
            "\t6. Exit GPA's Task Manager"
        );
    }

    static void CreateNewTask()
    {
        Console.WriteLine("Enter the task details: ");

        Console.Write("Task Description: ");
        string? description = Console.ReadLine();

        Console.Write("Due date and time (dd-MM-yyyy HH:mm): ");
        string? dateString = Console.ReadLine();

        try{
            if (string.IsNullOrWhiteSpace(description)){
                throw new NullReferenceException("Task cannot be empty");
            }

            DateTime dueDate = DateTime.ParseExact(dateString!, "dd-MM-yyyy HH:mm", null);

            TaskManager.AddNewTask(description, dueDate);
        } catch (NullReferenceException){
            Console.WriteLine("No task description entered");
            return;
        } catch (ArgumentNullException){
            Console.WriteLine("No date entered");
            return;
        } 
        catch (FormatException){
            Console.WriteLine("Entered date does not match specification");
            return;
        }

        Console.WriteLine("New task succesfully added");
    }

    static void SearchTask(){
        Console.WriteLine("Search task by description or status, sorted by due date");
        Console.WriteLine("Press C to cancel");
        Console.Write("Enter keyword: ");
        string? keyword = Console.ReadLine();

        if (string.IsNullOrEmpty(keyword)){
            Console.WriteLine("Please enter a valid keyword");
            return;
        } else if (keyword.ToLower() == "c"){
            return;
        }

        TaskManager.DisplayFilterResult(keyword);
    }
    static void ViewAllTask()
    {
        TaskManager.ViewAllTask();
    }

    static void FilterTaskByStatus()
    {
        Console.WriteLine("Not Yet Implemented");
    }

    static void MarkTaskAsDone()
    {
        TaskManager.ViewAllTask();
        Console.Write("Enter the ID of the task you want to mark as complete: ");
        try
        {
            int taskID = int.Parse(Console.ReadLine()!);

            int result = TaskManager.MarkTaskAsDone(taskID);
            if (result == -1)
            {
                Console.WriteLine("Task with id {0} does not exist.", taskID);
                return;
            }
            else if (result == 0)
            {
                Console.WriteLine("Task with id {0} is already marked as 'done'", taskID);
                return;
            }
            else
            {
                Console.WriteLine("Succesfully marked task with id {0} as 'done'", taskID);
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a valid id");
            return;
        }


    }
}