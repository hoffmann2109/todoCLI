namespace CLI_TODO.Logic.InputOutput;

public class OutputService
{
    public void PrintWelcomeMessage()
    {
        Console.WriteLine("Welcome back!");
    }

    public void PrintCommands()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Available commands:");
        Console.WriteLine("Help ... See available commands");
        Console.WriteLine("Add ... Add an item");
        Console.WriteLine("List ... List all todo items");
        Console.WriteLine("List --flag ... where flag = (--category, --all, --dueSoon)");
        Console.WriteLine("Complete ... Mark todo item as complete (enter description)");
        Console.WriteLine("Reopen ... Reopen todo item");
        Console.WriteLine("Delete ... Delete a todo item");
        Console.WriteLine("End ... End app");
        Console.WriteLine("----------------------------------------");
    }
}