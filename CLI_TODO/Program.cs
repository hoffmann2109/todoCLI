using CLI_TODO.Logic.InputOutput;

var outputService = new OutputService();
var inputService = new InputService();

outputService.PrintWelcomeMessage();

// Main loop:
while (true)
{
    inputService.GetUserInput();
}

