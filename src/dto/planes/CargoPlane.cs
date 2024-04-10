﻿using OOD_24L_01180689.src.reports;

namespace OOD_24L_01180689.src.dto.planes
{
    public class CargoPlane : Plane, IReportable
    {
        public Single MaxLoad { get; protected set; }

        public CargoPlane(string type, UInt64 id, string serial, string countryISO, string model, Single maxLoad) :
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