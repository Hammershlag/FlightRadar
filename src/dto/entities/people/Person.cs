namespace OOD_24L_01180689.src.dto.entities.people;

public abstract class Person : Entity
{
    protected Person(string type, ulong id, string name, ulong age, string phone, string email) :
        base(type, id)
    {
        Name = name;
        Age = age;
        Phone = phone;
        Email = email;
    }

    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"Person: {Type} {ID} {Name} {Age} {Phone} {Email}";
    }
}