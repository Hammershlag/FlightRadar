using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class RemoveCommand : Command
    {
        string objectClass;
        ConditionsList conditionsList;
        public RemoveCommand(string objectClass, ConditionsList conditionsList)
        {
            this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
            this.objectClass = objectClass;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
