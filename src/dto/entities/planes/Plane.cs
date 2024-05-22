namespace OOD_24L_01180689.src.dto.entities.planes;

public abstract class Plane : Entity
{
    protected Plane(string type, ulong id, string serial, string countryISO, string model) :
        base(type, id)
    {
        Serial = serial;
        CountryISO = countryISO;
        Model = model;
    }

    public string Serial { get; set; }
    public string CountryISO { get; set; }
    public string Model { get; set; }

    public override string ToString()
    {
        return $"Plane: {Type} {ID} {Serial} {CountryISO} {Model}";
    }
}