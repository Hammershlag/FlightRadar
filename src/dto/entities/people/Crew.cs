namespace OOD_24L_01180689.src.dto.entities.people
{
    public class Crew : Person
    {
        public ushort Practice { get; protected set; }
        public string Role { get; protected set; }

        public Crew(string type, ulong id, string name, ulong age, string phone, string email, ushort practice,
            string role) :
            base(type, id, name, age, phone, email)
        {
            Practice = practice;
            Role = role;
        }


        public override string ToString()
        {
            return $"Crew: {Type} {ID} {Name} {Age} {Phone} {Email} {Practice} {Role}";
        }
    }
}