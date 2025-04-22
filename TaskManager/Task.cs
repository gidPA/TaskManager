
public enum TaskStatus{
    InProgress,
    Done
}

public static class TaskStatusNames{
    public static string GetTaskName(this TaskStatus taskStatus){
    return taskStatus switch
        {
            TaskStatus.InProgress => "In Progress",
            TaskStatus.Done => "Done",
            _ => taskStatus.ToString()
        };
    }
}


class Task{
    private readonly IDictionary<TaskStatus, string> StatusNames = new Dictionary<TaskStatus, string>(){
        {TaskStatus.InProgress, "In Progress"},
        {TaskStatus.Done, "Done"},
    };
    public Task(string description, DateTime dueDate, int taskId){
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.InProgress;
        TaskId = taskId;
    }
    public int TaskId{get;set;}
    public string Description {get; set;}
    public TaskStatus Status {get; set;}
    public DateTime DueDate {get; set;}

    // public string GetTaskName(TaskStatus status){
    //     return StatusNames[status];
    // }
    
}