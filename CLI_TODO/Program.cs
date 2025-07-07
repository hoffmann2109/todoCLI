using CLI_TODO.Database;
using CLI_TODO.Logic.InputOutput;

var dbService = new DatabaseService();
dbService.InitializeDatabase();

var outputService = new OutputService();
var inputService = new InputService(dbService);

outputService.PrintWelcomeMessage();

// Main loop:
while (true)
{
    inputService.GetUserInput();
}

