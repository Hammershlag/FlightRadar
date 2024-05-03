using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command
{
    public abstract class Command
    {
        public string object_class { get; set; }

        public abstract bool Execute();
    }
}
