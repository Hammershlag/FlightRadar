using System.Text;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.airports
{
    public class Airport : Entity, IReportable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string CountryISO { get; set; }

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

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["NAME"] = () => Name;
            fieldGetters["CODE"] = () => Code;
            fieldGetters["LONGITUDE"] = () => Longitude;
            fieldGetters["LATITUDE"] = () => Latitude;
            fieldGetters["AMSL"] = () => AMSL;
            fieldGetters["COUNTRY ISO"] = () => CountryISO;
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