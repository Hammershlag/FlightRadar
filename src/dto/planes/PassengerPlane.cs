using NetworkSourceSimulator;
using OOD_24L_01180689.src.factories;
using System.Text;

namespace OOD_24L_01180689.src.dto.planes
{
    //"PP"
    //"NPP"
    public class PassengerPlane : Plane
    {
        public UInt16 FirstClassSize { get; protected set; }
        public UInt16 BusinessClassSize { get; protected set; }
        public UInt16 EconomyClassSize { get; protected set; }

        public PassengerPlane(string type, UInt64 id, string serial, string countryISO, string model, UInt16 firstClassSize, UInt16 businessClassSize, UInt16 economyClassSize) :
            base(type, id, serial, countryISO, model)
        {
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public override string ToString()
        {
            return
                $"PassengerPlane: {Type} {ID} {Serial} {CountryISO} {Model} {FirstClassSize} {BusinessClassSize} {EconomyClassSize}";
        }
    }

    public class PassengerPlaneFactory : EntityFactory
    {
        public override PassengerPlane Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new PassengerPlane(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                args[4],
                Convert.ToUInt16(args[5]),
                Convert.ToUInt16(args[6]),
                Convert.ToUInt16(args[7])
            );
        }

        public override PassengerPlane Create(Message message)
        {
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 36)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            UInt64 id = BitConverter.ToUInt64(messageBytes, 7);
            string serial = Encoding.ASCII.GetString(messageBytes, 15, 10).TrimEnd('\0');
            string countryISO = Encoding.ASCII.GetString(messageBytes, 25, 3);

            ushort modelLength = BitConverter.ToUInt16(messageBytes, 28);
            string model = Encoding.ASCII.GetString(messageBytes, 30, modelLength);

            UInt16 firstClassSize = BitConverter.ToUInt16(messageBytes, 30 + modelLength);
            UInt16 businessClassSize = BitConverter.ToUInt16(messageBytes, 32 + modelLength);
            UInt16 economyClassSize = BitConverter.ToUInt16(messageBytes, 34 + modelLength);

            return new PassengerPlane(code, id, serial, countryISO, model, firstClassSize, businessClassSize, economyClassSize);
        }
    }
}