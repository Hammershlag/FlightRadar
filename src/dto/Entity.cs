using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto
{
    public abstract class Entity
    {
        public string Type { get; set; }
        public UInt64 ID { get; set; }

        protected Entity(string type, UInt64 ID)
        {
            this.ID = ID;
            Type = type;
        }

        public override string ToString()
        {
            return $"Entity: {Type} {ID}";
        }
    }
}