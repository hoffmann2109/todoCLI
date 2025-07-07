namespace CLI_TODO.Data.Items;

public class PersonalItem : ITodoItem
{
    public TodoType TodoType => TodoType.Personal;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}