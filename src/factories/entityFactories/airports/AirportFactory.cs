using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.airports;
using System.Globalization;
using System.Text;

namespace OOD_24L_01180689.src.factories.entityFactories.airports
{
    //"AI" - FTR
    //"NAI" - networkSource
    public class AirportFactory : EntityFactory
    {
        public override Airport Create(params string[] args)
        {
            if (args.Length != 8) return null;
            var culture = CultureInfo.InvariantCulture;
            return new Airport(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                Convert.ToSingle(args[4], culture),
                Convert.ToSingle(args[5], culture),
                Convert.ToSingle(args[6], culture),
                args[7]
            );
        }

        public override Airport Create(Message message)
        {
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 35)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            ushort nameLength = BitConverter.ToUInt16(messageBytes, 15);
            string name = Encoding.ASCII.GetString(messageBytes, 17, nameLength);
            string airportCode = Encoding.ASCII.GetString(messageBytes, 17 + nameLength, 3);
            float longitude = BitConverter.ToSingle(messageBytes, 20 + nameLength);
            float latitude = BitConverter.ToSingle(messageBytes, 24 + nameLength);
            float amsl = BitConverter.ToSingle(messageBytes, 28 + nameLength);
            string countryISO = Encoding.ASCII.GetString(messageBytes, 32 + nameLength, 3);

            return new Airport(code, id, name, airportCode, longitude, latitude, amsl, countryISO);
        }
    }
}