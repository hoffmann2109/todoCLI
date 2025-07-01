namespace CLI_TODO.Data;

public class ExamItem : TodoItem
{
    public TodoType TodoType => TodoType.Exam;
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}