using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.dto.entities.planes
{
    public abstract class Plane : Entity
    {
        public string Serial { get; protected set; }
        public string CountryISO { get; protected set; }
        public string Model { get; protected set; }

        protected Plane(string type, ulong id, string serial, string countryISO, string model) :
            base(type, id)
        {
            Serial = serial;
            CountryISO = countryISO;
            Model = model;
        }

        public override string ToString()
        {
            return $"Plane: {Type} {ID} {Serial} {CountryISO} {Model}";
        }
    }
}