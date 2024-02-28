namespace OOD_24L_01180689.src.dto.people
{
    public abstract class Person : Entity
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        protected Person(string type, UInt64 id, string name, UInt64 age, string phone, string email) :
            base(type, id)
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }

        public override string ToString()
        {
            return $"Person: {Type} {ID} {Name} {Age} {Phone} {Email}";
        }
    }
}