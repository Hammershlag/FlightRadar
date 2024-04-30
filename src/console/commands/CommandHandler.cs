using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.console.commands.command;

namespace OOD_24L_01180689.src.console.commands
{
    public class CommandHandler
    {

        List<Command> commands = new List<Command>();

        public bool Update()
        {
            return false;
        }

        public bool Add()
        {
            return false;
        }

        public bool Delete()
        {
            return false;
        }

        public bool Display()
        {
            return false;
        }

        public void AddCommand(Command command)
        {
            commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            commands.Remove(command);
        }

        public IEnumerable<bool> ExecuteNext()
        {
            for (int i = 0; i < commands.Count; i++)
            {
                yield return ExecuteCommand(commands[i]);
            }
        }

        private bool ExecuteCommand(Command command)
        {
            return command.Execute();
        }
    }
}
