namespace CLI_TODO.Data;

public class PersonalItem : TodoItem
{
    public TodoType TodoType => TodoType.Personal;
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}