using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.people
{
    //"C"
    public class Crew : Person
    {
        public UInt16 Practice { get; set; }
        public string Role { get; set; }
        public Crew(string type, UInt64 id, string name, ulong age, string phone, string email, UInt16 practice, string role) :
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
