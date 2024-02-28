using OOD_24L_01180689.src.factories;
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

        public PassengerPlane(string type, UInt64 id, string serial, string countryISO, string model, UInt16 firstClassSize, UInt16 businessClassSize, UInt16 economyClassSize) :
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
    }

    public class PassengerPlaneFactory : EntityFactory
    {
        public override PassengerPlane Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new PassengerPlane(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                args[4],
                Convert.ToUInt16(args[5]),
                Convert.ToUInt16(args[6]),
                Convert.ToUInt16(args[7])
            );
        }
    }
}