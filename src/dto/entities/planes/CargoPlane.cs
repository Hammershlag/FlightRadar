using OOD_24L_01180689.src.dto.reports.reporters;
using System.Text;
using System.Xml.Linq;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.dataStorage;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public class CargoPlane : Plane, IReportable
    {
        public float MaxLoad { get; set; }

        public CargoPlane() : base("Wrong", ulong.MaxValue, "Wrong", "Wrong", "Wrong")
        {
        }
        public CargoPlane(string type, ulong id, string serial, string countryISO, string model, float maxLoad) :
            base(type, id, serial, countryISO, model)
        {
            MaxLoad = maxLoad;
        }


        public override string ToString()
        {
            return $"CargoPlane: {Type} {ID} {Serial} {CountryISO} {Model} {MaxLoad}";
        }

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["SERIAL"] = () => Serial;
            fieldGetters["COUNTRYISO"] = () => CountryISO;
            fieldGetters["MODEL"] = () => Model;
            fieldGetters["MAXLOAD"] = () => MaxLoad;
        }

        protected override void InitializeFieldSetters()
        {
            fieldSetters["ID"] = (value) => ID = value == null || DataStorage.GetInstance.GetIDEntityMap().TryGetValue((ulong)value, out Entity ignore) ? DataStorage.GetInstance.MaxID() + 1 : (ulong)value;
            fieldSetters["TYPE"] = (value) => Type = "CP";
            fieldSetters["SERIAL"] = (value) => Serial = value == null ? "Uninitialized" : (string)value;
            fieldSetters["COUNTRYISO"] = (value) => CountryISO = value == null ? "Uninitialized" : (string)value;
            fieldSetters["MODEL"] = (value) => Model = value == null ? "Uninitialized" : (string)value;
            fieldSetters["MAXLOAD"] = (value) => MaxLoad = value == null ? float.MaxValue : (float)value;
        }




        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}