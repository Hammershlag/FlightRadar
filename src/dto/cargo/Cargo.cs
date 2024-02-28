using OOD_24L_01180689.src.dto.planes;
using OOD_24L_01180689.src.factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.cargo
{
    //"CA"
    public class Cargo : Entity
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(string type, UInt64 id, float weight, string code, string description) :
            base(type, id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }


        public override string ToString()
        {
            return $"Cargo: {Type} {ID} {Weight} {Code} {Description}";
        }
    }

    public class CargoFactory : EntityFactory
    {
        public override Cargo Create(params string[] args)
        {
            if (args.Length != 5) return null;
            var culture = CultureInfo.InvariantCulture;

            return new Cargo(
                args[0],
                Convert.ToUInt64(args[1]),
                Convert.ToSingle(args[2], culture),
                args[3],
                args[4]
            );
        }
    }
}