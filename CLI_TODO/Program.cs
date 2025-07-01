using CLI_TODO.Database;
using CLI_TODO.Logic.InputOutput;

var db = new DatabaseService();
var outputService = new OutputService();
var inputService = new InputService();

db.InitializeDatabase();
outputService.PrintWelcomeMessage();

// Main loop:
while (true)
{
    inputService.GetUserInput();
}

