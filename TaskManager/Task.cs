using System.Dynamic;

enum TaskStatus{
    InProgres,
    Done
}

class Task{
    private readonly IDictionary<TaskStatus, string> StatusNames = new Dictionary<TaskStatus, string>(){
        {TaskStatus.InProgres, "In Progress"},
        {TaskStatus.Done, "Done"},
    };
    public Task(string description, DateTime dueDate, int taskId){
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.InProgres;
        TaskId = taskId;
    }
    public int TaskId{get;set;}
    public string Description {get; set;}
    public TaskStatus Status {get; set;}
    public DateTime DueDate {get; set;}

    public string GetTaskName(TaskStatus status){
        return StatusNames[status];
    }
    
}