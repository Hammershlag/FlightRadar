using System.Text;
using System.Xml.Linq;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.dataStorage;

namespace OOD_24L_01180689.src.dto.entities.cargo
{
    public class Cargo : Entity
    {
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo() : base("Wrong", ulong.MaxValue)
        {
        }
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

        protected override void InitializeFieldSetters()
        {
            fieldSetters["ID"] = (value) => ID = value == null || DataStorage.GetInstance.GetIDEntityMap().TryGetValue((ulong)value, out Entity ignore) ? DataStorage.GetInstance.MaxID() + 1 : (ulong)value;
            fieldSetters["TYPE"] = (value) => Type = "CA";
            fieldSetters["WEIGHT"] = (value) => Weight = value == null ? float.MaxValue : (float)value;
            fieldSetters["CODE"] = (value) => Code = value == null ? "Uninitialized" : (string)value;
            fieldSetters["DESCRIPTION"] = (value) => Description = value == null ? "Uninitialized" : (string)value;
        }


    }
}