using CLI_TODO.Data.Items;
using CLI_TODO.Logic.InputOutput;

namespace CLI_TODO.Data;

public class InputMessage
{
    public Commands Command { get; set; }
    public ITodoItem? TodoItem { get; set; }
}