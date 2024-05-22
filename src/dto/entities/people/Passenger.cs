namespace OOD_24L_01180689.src.dto.entities.people;

public class Passenger : Person
{
    public Passenger() : base("Wrong", ulong.MaxValue, "Wrong", ulong.MaxValue, "Wrong", "Wrong")
    {
    }

    public Passenger(string type, ulong id, string name, ulong age, string phone, string email, string _class,
        ulong miles) :
        base(type, id, name, age, phone, email)
    {
        Class = _class;
        Miles = miles;
    }

    public string Class { get; set; }
    public ulong Miles { get; set; }


    public override string ToString()
    {
        return $"Passenger: {Type} {ID} {Name} {Age} {Phone} {Email} {Class} {Miles}";
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as Passenger != null)
        {
            output = (Passenger)input;
            return true;
        }

        output = default(Passenger);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["NAME"] = () => Name;
        fieldGetters["AGE"] = () => Age;
        fieldGetters["PHONE"] = () => Phone;
        fieldGetters["EMAIL"] = () => Email;
        fieldGetters["CLASS"] = () => Class;
        fieldGetters["MILES"] = () => Miles;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "P";
        fieldSetters["NAME"] = value => Name = value == null ? "Uninitialized" : (string)value;
        fieldSetters["AGE"] = value => Age = value == null ? ulong.MaxValue : (ulong)value;
        fieldSetters["PHONE"] = value => Phone = value == null ? "Uninitialized" : (string)value;
        fieldSetters["EMAIL"] = value => Email = value == null ? "Uninitialized" : (string)value;
        fieldSetters["CLASS"] = value => Class = value == null ? "Uninitialized" : (string)value;
        fieldSetters["MILES"] = value => Miles = value == null ? ulong.MaxValue : (ulong)value;
    }
}