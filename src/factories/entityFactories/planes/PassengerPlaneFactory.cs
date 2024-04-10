using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.planes;
using System.Text;

namespace OOD_24L_01180689.src.factories.entityFactories.planes
{
    //"PP" - FTR
    //"NPP" - networkSource
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
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            string serial = Encoding.ASCII.GetString(messageBytes, 15, 10).TrimEnd('\0');
            string countryISO = Encoding.ASCII.GetString(messageBytes, 25, 3);
            ushort modelLength = BitConverter.ToUInt16(messageBytes, 28);
            string model = Encoding.ASCII.GetString(messageBytes, 30, modelLength);
            ushort firstClassSize = BitConverter.ToUInt16(messageBytes, 30 + modelLength);
            ushort businessClassSize = BitConverter.ToUInt16(messageBytes, 32 + modelLength);
            ushort economyClassSize = BitConverter.ToUInt16(messageBytes, 34 + modelLength);

            return new PassengerPlane(code, id, serial, countryISO, model, firstClassSize, businessClassSize,
                economyClassSize);
        }
    }
}