using NetworkSourceSimulator;
using OOD_24L_01180689.src.factories;
using System.Text;

namespace OOD_24L_01180689.src.dto.people
{
    //"C" - FTR
    //"NCR" - networkSource
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

        public override Crew Create(Message message)
        {
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 36)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            UInt64 id = BitConverter.ToUInt64(messageBytes, 7);
            ushort nameLength = BitConverter.ToUInt16(messageBytes, 15);
            string name = Encoding.ASCII.GetString(messageBytes, 17, nameLength);
            ulong age = BitConverter.ToUInt16(messageBytes, 17 + nameLength);
            string phone = Encoding.ASCII.GetString(messageBytes, 19 + nameLength, 12);
            ushort emailLength = BitConverter.ToUInt16(messageBytes, 31 + nameLength);
            string email = Encoding.ASCII.GetString(messageBytes, 33 + nameLength, emailLength);
            UInt16 practice = BitConverter.ToUInt16(messageBytes, 33 + nameLength + emailLength);
            string role = Encoding.ASCII.GetString(messageBytes, 35 + nameLength + emailLength, 1);

            return new Crew(code, id, name, age, phone, email, practice, role);
        }
    }
}