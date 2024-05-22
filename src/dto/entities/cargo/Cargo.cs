namespace OOD_24L_01180689.src.dto.entities.cargo;

public class Cargo : Entity
{
    public Cargo() : base("Wrong", ulong.MaxValue)
    {
    }

    public Cargo(string type, ulong id, float weight, string code, string description) :
        base(type, id)
    {
        Weight = weight;
        Code = code;
        Description = description;
    }

    public float Weight { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }


    public override string ToString()
    {
        return $"Cargo: {Type} {ID} {Weight} {Code} {Description}";
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as Cargo != null)
        {
            output = (Cargo)input;
            return true;
        }

        output = default(Cargo);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["WEIGHT"] = () => Weight;
        fieldGetters["CODE"] = () => Code;
        fieldGetters["DESCRIPTION"] = () => Description;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "CA";
        fieldSetters["WEIGHT"] = value => Weight = value == null ? float.MaxValue : (float)value;
        fieldSetters["CODE"] = value => Code = value == null ? "Uninitialized" : (string)value;
        fieldSetters["DESCRIPTION"] = value => Description = value == null ? "Uninitialized" : (string)value;
    }
}