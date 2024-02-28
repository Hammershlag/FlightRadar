using OOD_24L_01180689.src.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.planes
{
    //"CP"
    public class CargoPlane : Plane
    {
        public Single MaxLoad { get; set; }

        public CargoPlane(string type, UInt64 id, string serial, string countryISO, string model, Single maxLoad) :
            base(type, id, serial, countryISO, model)
        {
            MaxLoad = maxLoad;
        }


        public override string ToString()
        {
            return $"CargoPlane: {Type} {ID} {Serial} {CountryISO} {Model} {MaxLoad}";
        }
    }

    public class CargoPlaneFactory : EntityFactory
    {
        public override CargoPlane Create(params string[] args)
        {
            if (args.Length != 6) return null;
            return new CargoPlane(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                args[4],
                Convert.ToSingle(args[5])
            );
        }
    }
}