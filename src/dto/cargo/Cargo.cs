using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.cargo
{
    //"CA"
    public class Cargo : Entity
    {

        public float Weight { get; set; }
        public string Volume { get; set; }
        public string Description { get; set; }

        public Cargo(string type, UInt64 id, float weight, string volume, string description) :
            base(type, id)
        {
            Weight = weight;
            Volume = volume;
            Description = description;
        }


        public override string ToString()
        {
            return $"Cargo: {Type} {ID} {Weight} {Volume} {Description}";
        }

    }
}
