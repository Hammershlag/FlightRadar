using OOD_24L_01180689.src.dto.reports.reporters;

namespace OOD_24L_01180689.src.dto.entities.planes;

public class CargoPlane : Plane, IReportable
{
    public CargoPlane() : base("Wrong", ulong.MaxValue, "Wrong", "Wrong", "Wrong")
    {
    }

    public CargoPlane(string type, ulong id, string serial, string countryISO, string model, float maxLoad) :
        base(type, id, serial, countryISO, model)
    {
        MaxLoad = maxLoad;
    }

    public float MaxLoad { get; set; }


    public void Accept(INewsVisitor visitor)
    {
        visitor.Visit(this);
    }


    public override string ToString()
    {
        return $"CargoPlane: {Type} {ID} {Serial} {CountryISO} {Model} {MaxLoad}";
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as CargoPlane != null)
        {
            output = (CargoPlane)input;
            return true;
        }

        output = default(CargoPlane);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["SERIAL"] = () => Serial;
        fieldGetters["COUNTRYISO"] = () => CountryISO;
        fieldGetters["MODEL"] = () => Model;
        fieldGetters["MAXLOAD"] = () => MaxLoad;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "CP";
        fieldSetters["SERIAL"] = value => Serial = value == null ? "Uninitialized" : (string)value;
        fieldSetters["COUNTRYISO"] = value => CountryISO = value == null ? "Uninitialized" : (string)value;
        fieldSetters["MODEL"] = value => Model = value == null ? "Uninitialized" : (string)value;
        fieldSetters["MAXLOAD"] = value => MaxLoad = value == null ? float.MaxValue : (float)value;
    }
}