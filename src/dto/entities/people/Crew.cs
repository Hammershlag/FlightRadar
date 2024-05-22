namespace OOD_24L_01180689.src.dto.entities.people;

public class Crew : Person
{
    public Crew() : base("Wrong", ulong.MaxValue, "Wrong", ulong.MaxValue, "Wrong", "Wrong")
    {
    }

    public Crew(string type, ulong id, string name, ulong age, string phone, string email, ushort practice,
        string role) :
        base(type, id, name, age, phone, email)
    {
        Practice = practice;
        Role = role;
    }

    public ushort Practice { get; set; }
    public string Role { get; set; }


    public override string ToString()
    {
        return $"Crew: {Type} {ID} {Name} {Age} {Phone} {Email} {Practice} {Role}";
    }

    public override bool TryParse(Entity input, out Entity output)
    {
        if (input as Crew != null)
        {
            output = (Crew)input;
            return true;
        }

        output = default(Crew);
        return false;
    }

    protected override void InitializeFieldGetters()
    {
        base.InitializeFieldGetters();
        fieldGetters["NAME"] = () => Name;
        fieldGetters["AGE"] = () => Age;
        fieldGetters["PHONE"] = () => Phone;
        fieldGetters["EMAIL"] = () => Email;
        fieldGetters["PRACTICE"] = () => Practice;
        fieldGetters["ROLE"] = () => Role;
    }

    protected override void InitializeFieldSetters()
    {
        base.InitializeFieldSetters();
        fieldSetters["TYPE"] = value => Type = "C";
        fieldSetters["NAME"] = value => Name = value == null ? "Uninitialized" : (string)value;
        fieldSetters["AGE"] = value => Age = value == null ? ulong.MaxValue : (ulong)value;
        fieldSetters["PHONE"] = value => Phone = value == null ? "Uninitialized" : (string)value;
        fieldSetters["EMAIL"] = value => Email = value == null ? "Uninitialized" : (string)value;
        fieldSetters["PRACTICE"] = value => Practice = value == null ? ushort.MaxValue : (ushort)value;
        fieldSetters["ROLE"] = value => Role = value == null ? "Uninitialized" : (string)value;
    }
}