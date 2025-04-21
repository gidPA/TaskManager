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
                    FilterTaskByStatus();
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
            "\t4. Filter task by status\n" +
            "\t5. Mark Task as complete\n" +
            "\t6. Exit GPA's Task Manager"
        );
    }

    static void CreateNewTask()
    {
        Console.WriteLine("Enter the task details: ");

        int[] dateArgs = new int[5];
        string?[] ArgString = new string[6];

        Console.Write("Task Description: ");
        ArgString[0] = Console.ReadLine();

        Console.Write("Year: ");
        ArgString[1] = Console.ReadLine();

        Console.Write("Month (in number): ");
        ArgString[2] = Console.ReadLine();
        Console.Write("Day: ");
        ArgString[3] = Console.ReadLine();
        Console.Write("Hour: ");
        ArgString[4] = Console.ReadLine();
        Console.Write("Minute: ");
        ArgString[5] = Console.ReadLine();

        foreach (string? arg in ArgString)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                Console.WriteLine("Cannot create a new task: Not all details were valid.");
                return;
            }
        }

        for (int i = 1; i < ArgString.Length; i++)
        {
            dateArgs[i - 1] = int.Parse(ArgString[i]);
        }

        try{
            TaskManager.AddNewTask(
                    ArgString[0],   //Description
                    dateArgs[0],    //Year
                    dateArgs[1],    //Month
                    dateArgs[2],    //Day
                    dateArgs[3],    //Hour
                    dateArgs[4]     //Minute
                );
        } catch (FormatException)
        {
            Console.WriteLine("Cannot create a new task: Not all date and time details were valid numbers");
            return;
        }


        Console.WriteLine("New task succesfully added");

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
            int taskID = int.Parse(Console.ReadLine());
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