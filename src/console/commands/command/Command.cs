namespace OOD_24L_01180689.src.console.commands.command;

public abstract class Command
{
    public string object_class { get; set; }

    public abstract bool Execute();
}