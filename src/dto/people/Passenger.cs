using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.people
{
    //"P"
    public class Passenger : Person
    {

        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger(string type, UInt64 id, string name, ulong age, string phone, string email, string _class, ulong miles) :
            base(type, id, name, age, phone, email)
        {
            Class = _class;
            Miles = miles;
        }


        public override string ToString()
        {
            return $"Passenger: {Type} {ID} {Name} {Age} {Phone} {Email} {Class} {Miles}";
        }

    }
}
