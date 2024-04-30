using System.Text;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.people
{
    public class Crew : Person
    {
        public ushort Practice { get; set; }
        public string Role { get; set; }

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

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["NAME"] = () => Name;
            fieldGetters["AGE"] = () => Age;
            fieldGetters["PHONE"] = () => Phone;
            fieldGetters["EMAIL"] = () => Email;
            fieldGetters["PRACTICE"] = () => Practice;
            fieldGetters["ROLE"] = () => Role;
        }

    }
}