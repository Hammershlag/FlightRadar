namespace OOD_24L_01180689.src.dto.people
{
    public class Crew : Person
    {
        public UInt16 Practice { get; protected set; }
        public string Role { get; protected set; }

        public Crew(string type, UInt64 id, string name, ulong age, string phone, string email, UInt16 practice,
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