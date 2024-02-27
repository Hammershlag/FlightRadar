using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Airport(string type, UInt64 id, string name, string code, float longitude, float latitude, float amsl,
            string countryISO) :
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
}
