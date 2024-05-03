using System.Text;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.dataStorage;

namespace OOD_24L_01180689.src.dto.entities.people
{
    public class Passenger : Person
    {
        public string Class { get; set; }
        public ulong Miles { get; set; }

        public Passenger() : base("Wrong", ulong.MaxValue, "Wrong", ulong.MaxValue, "Wrong", "Wrong")
        {
        }

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

        public override bool TryParse(Entity input, out Entity output)
        {
            if (input as Passenger != null)
            {
                output = (Passenger)input;
                return true;
            }

            output = default(Passenger);
            return false;
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

        protected override void InitializeFieldSetters()
        {
            fieldSetters["ID"] = (value) =>
            {
                ulong prev = ID;
                ID = value == null || DataStorage.GetInstance.GetIDEntityMap()
                    .TryGetValue((ulong)value, out Entity ignore)
                    ? DataStorage.GetInstance.MaxID() + 1
                    : (ulong)value;
                if (prev == null) return;

                foreach (var flight in DataStorage.GetInstance.GetFlights())
                {
                    flight.UpdateIDs(prev, ID);
                }
            }; fieldSetters["TYPE"] = (value) => Type = "P";
            fieldSetters["NAME"] = (value) => Name = value == null ? "Uninitialized" : (string)value;
            fieldSetters["AGE"] = (value) => Age = value == null ? ulong.MaxValue : (ulong)value;
            fieldSetters["PHONE"] = (value) => Phone = value == null ? "Uninitialized" : (string)value;
            fieldSetters["EMAIL"] = (value) => Email = value == null ? "Uninitialized" : (string)value;
            fieldSetters["CLASS"] = (value) => Class = value == null ? "Uninitialized" : (string)value;
            fieldSetters["MILES"] = (value) => Miles = value == null ? ulong.MaxValue : (ulong)value;
        }


    }
}