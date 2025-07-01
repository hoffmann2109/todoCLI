namespace CLI_TODO.Logic.InputOutput;

public class CommandHandler
{
    public void ProcessHelp()
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Available commands:");
        Console.WriteLine("Help ... See available commands");
        Console.WriteLine("List ... List all todo items");
        Console.WriteLine("Complete ... Mark todo item as complete");
        Console.WriteLine("Reopen ... Reopen todo item");
        Console.WriteLine("Update ... Update a todo item");
        Console.WriteLine("Delete ... Delete a todo item");
        Console.WriteLine("End ... End app");
        Console.WriteLine("----------------------------------------");
    }
    
    public void ProcessAdd()
    {
        
    }
    
    public void ProcessList()
    {

    }
    
    public void ProcessComplete()
    {

    }
    
    public void ProcessReopen()
    {

    }
    
    public void ProcessUpdate()
    {

    }
    
    public void ProcessDelete()
    {

    }

    public void ProcessEnd()
    {
        Environment.Exit(0);
    }
    
}