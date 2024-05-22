using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.airports;

public class Airport : Entity, IReportable
{
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

    public string Name { get; set; }
    public string Code { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public string CountryISO { get; set; }

    public void Accept(INewsVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as Airport != null)
        {
            output = (Airport)input;
            return true;
        }

        output = default(Airport);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["NAME"] = () => Name;
        fieldGetters["CODE"] = () => Code;
        fieldGetters["LONGITUDE"] = () => Longitude;
        fieldGetters["LATITUDE"] = () => Latitude;
        fieldGetters["AMSL"] = () => AMSL;
        fieldGetters["COUNTRYISO"] = () => CountryISO;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "AI";
        fieldSetters["NAME"] = value => Name = value == null ? "Uninitialized" : (string)value;
        fieldSetters["CODE"] = value => Code = value == null ? "Uninitialized" : (string)value;
        fieldSetters["LONGITUDE"] = value => Longitude = value == null ? float.MaxValue : (float)value;
        fieldSetters["LATITUDE"] = value => Latitude = value == null ? float.MaxValue : (float)value;
        fieldSetters["AMSL"] = value => AMSL = value == null ? float.MaxValue : (float)value;
        fieldSetters["COUNTRYISO"] = value => CountryISO = value == null ? "Uninitialized" : (string)value;
    }

    public override string ToString()
    {
        return $"Airport: {Type} {ID} {Name} {Code} {Longitude} {Latitude} {AMSL} {CountryISO}";
    }
}