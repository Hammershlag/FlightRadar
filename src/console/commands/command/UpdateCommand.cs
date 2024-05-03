using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class UpdateCommand : Command
    {
        private string objectClass;
        private Dictionary<string, IComparable> keyValSet;
        private ConditionsList conditionsList;
        public UpdateCommand(string objectClass, Dictionary<string, IComparable> keyValSet, ConditionsList conditionsList)
        {
            this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
            this.keyValSet = keyValSet;
            this.objectClass = objectClass;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
