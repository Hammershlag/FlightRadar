using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class AddCommand : Command
    {

        public Dictionary<object, object> key_value_list = new Dictionary<object, object>();
        public ConditionsList conditionsList = new ConditionsList();


        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
