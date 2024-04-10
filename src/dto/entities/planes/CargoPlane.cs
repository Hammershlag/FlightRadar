using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public class CargoPlane : Plane, IReportable
    {
        public float MaxLoad { get; protected set; }

        public CargoPlane(string type, ulong id, string serial, string countryISO, string model, float maxLoad) :
            base(type, id, serial, countryISO, model)
        {
            MaxLoad = maxLoad;
        }


        public override string ToString()
        {
            return $"CargoPlane: {Type} {ID} {Serial} {CountryISO} {Model} {MaxLoad}";
        }

        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}