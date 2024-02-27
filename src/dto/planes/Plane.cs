using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.planes
{
    public abstract class Plane : Entity
    {
        public string Serial { get; set; }
        public string CountryISO { get; set; }
        public string Model { get; set; }

        protected Plane(string type, UInt64 id, string serial, string countryISO, string model) :
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
