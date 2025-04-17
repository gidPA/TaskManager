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
    public Task(string description, DateTime dueDate){
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.InProgres;
    }
    public int TaskID{get;set;}
    public string Description {get; set;}
    public TaskStatus Status {get; set;}
    public DateTime DueDate {get; set;}

    public string GetTaskName(TaskStatus status){
        return StatusNames[status];
    }
    
}