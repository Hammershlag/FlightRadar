using OOD_24L_01180689.src.dto.reports.reporters;
using System.Text;
using System.Xml.Linq;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public class CargoPlane : Plane, IReportable
    {
        public float MaxLoad { get; set; }

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
            fieldGetters["COUNTRY ISO"] = () => CountryISO;
            fieldGetters["MODEL"] = () => Model;
            fieldGetters["MAX LOAD"] = () => MaxLoad;
        }


        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}