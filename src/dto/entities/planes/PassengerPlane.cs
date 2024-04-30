using OOD_24L_01180689.src.dto.reports.reporters;
using System.Text;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public class PassengerPlane : Plane, IReportable
    {
        public ushort FirstClassSize { get; set; }
        public ushort BusinessClassSize { get; set; }
        public ushort EconomyClassSize { get; set; }

        public PassengerPlane(string type, ulong id, string serial, string countryISO, string model,
            ushort firstClassSize, ushort businessClassSize, ushort economyClassSize) :
            base(type, id, serial, countryISO, model)
        {
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public override string ToString()
        {
            return
                $"PassengerPlane: {Type} {ID} {Serial} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
        }

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["SERIAL"] = () => Serial;
            fieldGetters["COUNTRY ISO"] = () => CountryISO;
            fieldGetters["MODEL"] = () => Model;
            fieldGetters["FIRST CLASS SIZE"] = () => FirstClassSize;
            fieldGetters["BUSINESS CLASS SIZE"] = () => BusinessClassSize;
            fieldGetters["ECONOMY CLASS SIZE"] = () => EconomyClassSize;
        }


        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}