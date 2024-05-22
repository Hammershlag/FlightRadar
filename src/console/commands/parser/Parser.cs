using OOD_24L_01180689.src.console.commands.command;

namespace OOD_24L_01180689.src.console.commands.parser;

public abstract class Parser
{
    public abstract Command Parse(string command);
}