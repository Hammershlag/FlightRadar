using System.Text;
using System.Xml.Linq;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.cargo
{
    public class Cargo : Entity
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(string type, ulong id, float weight, string code, string description) :
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

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["WEIGHT"] = () => Weight;
            fieldGetters["CODE"] = () => Code;
            fieldGetters["DESCRIPTION"] = () => Description;
        }
    }
}