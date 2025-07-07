namespace CLI_TODO.Data;

public static class TodoItemFactory
{
    public static TodoItem Create(
        TodoType type,
        string description,
        DateTime dueDate)
    {
        TodoItem item = type switch
        {
            TodoType.Personal   => new PersonalItem(),
            TodoType.University => new UniversityItem(),
            TodoType.Exam       => new ExamItem(),
            TodoType.Work       => new WorkItem(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        item.Description = description;
        item.DueDate     = dueDate;
        item.IsCompleted = false;

        return item;
    }
}
