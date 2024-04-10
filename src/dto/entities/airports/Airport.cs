﻿using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.airports
{
    public class Airport : Entity, IReportable
    {
        public string Name { get; protected set; }
        public string Code { get; protected set; }
        public float Longitude { get; protected set; }
        public float Latitude { get; protected set; }
        public float AMSL { get; protected set; }
        public string CountryISO { get; protected set; }

        public Airport() : base("Wrong", ulong.MaxValue)
        {
        }

        public Airport(string type, ulong id, string name, string code, float longitude, float latitude, float amsl,
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

        public void Accept(INewsVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}