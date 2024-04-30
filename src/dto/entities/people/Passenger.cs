using System.Text;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.people
{
    public class Passenger : Person
    {
        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger(string type, ulong id, string name, ulong age, string phone, string email, string _class,
            ulong miles) :
            base(type, id, name, age, phone, email)
        {
            Class = _class;
            Miles = miles;
        }


        public override string ToString()
        {
            return $"Passenger: {Type} {ID} {Name} {Age} {Phone} {Email} {Class} {Miles}";
        }

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["NAME"] = () => Name;
            fieldGetters["AGE"] = () => Age;
            fieldGetters["PHONE"] = () => Phone;
            fieldGetters["EMAIL"] = () => Email;
            fieldGetters["CLASS"] = () => Class;
            fieldGetters["MILES"] = () => Miles;
        }

    }
}