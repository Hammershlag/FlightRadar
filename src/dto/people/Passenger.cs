using OOD_24L_01180689.src.factories;

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

    public class PassengerFactory : EntityFactory
    {
        public override Passenger Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new Passenger(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                args[6],
                Convert.ToUInt64(args[7])
            );
        }
    }
}