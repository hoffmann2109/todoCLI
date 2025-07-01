namespace CLI_TODO.Logic.InputOutput;

public enum Commands
{
    Help, // Print all available commands
    Add, // Add an item
    List, // List all due items sorted in ascending order with attribute date
    Complete, // Complete an item
    Reopen, // Reopen a completed item
    Update, // Edit an item
    Delete // Delete an item
}