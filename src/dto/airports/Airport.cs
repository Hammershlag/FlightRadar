using OOD_24L_01180689.src.factories;
using System.Globalization;

namespace OOD_24L_01180689.src.dto.airports
{
    //"AI"
    public class Airport : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }

        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string CountryISO { get; set; }

        public Airport(string type, UInt64 id, string name, string code, float longitude, float latitude, float amsl, string countryISO) :
            base(type, id)
        {
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            CountryISO = countryISO;
        }

        public override string ToString()
        {
            return $"Airport: {Type} {ID} {Name} {Code} {Longitude} {Latitude} {AMSL} {CountryISO}";
        }
    }

    public class AirportFactory : EntityFactory
    {
        public override Airport Create(params string[] args)
        {
            if (args.Length != 8) return null;
            var culture = CultureInfo.InvariantCulture;
            return new Airport(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                Convert.ToSingle(args[4], culture),
                Convert.ToSingle(args[5], culture),
                Convert.ToSingle(args[6], culture),
                args[7]
            );
        }
    }
}