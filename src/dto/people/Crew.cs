using OOD_24L_01180689.src.factories;
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

    public class CrewFactory : EntityFactory
    {
        public override Crew Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new Crew(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                Convert.ToUInt16(args[6]),
                args[7]
            );
        }
    }
}