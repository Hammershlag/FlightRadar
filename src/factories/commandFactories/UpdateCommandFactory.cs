using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console.commands.command;

namespace OOD_24L_01180689.src.factories.commandFactories
{
    internal class UpdateCommandFactory : CommandFactory
    {
        public override Command Create(string object_class, Dictionary<string, IComparable> key_val_set, List<string> object_fields, ConditionsList conditionsList)
        {
            return new UpdateCommand(object_class, key_val_set, conditionsList);
        }
    }
}
