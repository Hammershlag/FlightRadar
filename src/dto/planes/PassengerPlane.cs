using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.planes
{
    //"PP"
    public class PassengerPlane : Plane
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane(string type, UInt64 id, string serial, string countryISO, string model, UInt16 firstClassSize,
            UInt16 businessClassSize, UInt16 economyClassSize) :
            base(type, id, serial, countryISO, model)
        {
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public override string ToString()
        {
            return $"PassengerPlane: {Type} {ID} {Serial} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
        }
    }
}
