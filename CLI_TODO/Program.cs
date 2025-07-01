using CLI_TODO.Logic.InputOutput;
using DotNetEnv;

Env.Load();

var outputService = new OutputService();
var inputService = new InputService();
var conn = Environment.GetEnvironmentVariable("MONGO_CONN")
           ?? throw new InvalidOperationException("Set MONGO_CONN in .env");

outputService.PrintWelcomeMessage();

// Main loop:
while (true)
{
    inputService.GetUserInput();
}

