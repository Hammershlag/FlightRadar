using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public class PassengerPlane : Plane, IReportable
    {
        public ushort FirstClassSize { get; protected set; }
        public ushort BusinessClassSize { get; protected set; }
        public ushort EconomyClassSize { get; protected set; }

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

        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}