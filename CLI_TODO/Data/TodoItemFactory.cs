using CLI_TODO.Data.Items;

namespace CLI_TODO.Data;

public static class TodoItemFactory
{
    public static ITodoItem Create(
        TodoType type,
        string? description,
        DateTime dueDate)
    {
        ITodoItem item = type switch
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
