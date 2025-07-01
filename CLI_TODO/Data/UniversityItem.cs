namespace CLI_TODO.Data;

public class UniversityItem : TodoItem
{
    public TodoType TodoType => TodoType.University;
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}