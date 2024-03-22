using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.flights;
using System.Globalization;
using System.Text;

namespace OOD_24L_01180689.src.factories.entityFactories.flights
{
    //"FL" - FTR
    //"NFL" - networkSource
    public class FlightFactory : EntityFactory
    {
        public override Flight Create(params string[] args)
        {
            if (args.Length != 12) return null;
            var culture = CultureInfo.InvariantCulture;

            return new Flight(
                args[0],
                Convert.ToUInt64(args[1]),
                Convert.ToUInt64(args[2]),
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                Convert.ToSingle(args[6], culture),
                Convert.ToSingle(args[7], culture),
                Convert.ToSingle(args[8], culture),
                Convert.ToUInt64(args[9]),
                ParseStringToUInt64Array(args[10]),
                ParseStringToUInt64Array(args[11])
            );
        }

        public override Flight Create(Message message)
        {
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 59)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            ulong originID = BitConverter.ToUInt64(messageBytes, 15);
            ulong targetID = BitConverter.ToUInt64(messageBytes, 23);
            DateTimeOffset takeOffTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 31));
            DateTimeOffset landingTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 39));

            // Check if landing time is before takeoff time and adjust
            if (landingTimeDate < takeOffTimeDate)
            {
                landingTimeDate = landingTimeDate.AddDays(1);
            }

            string takeOffTime = takeOffTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
            string landingTime = landingTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
            ulong planeID = BitConverter.ToUInt64(messageBytes, 47);
            ushort crewCount = BitConverter.ToUInt16(messageBytes, 55);
            ulong[] crewID = ParseByteArrayToUInt64Array(messageBytes, 57, crewCount);
            ushort passengersCargoCount = BitConverter.ToUInt16(messageBytes, 57 + crewCount * 8);
            ulong[] loadID = ParseByteArrayToUInt64Array(messageBytes, 59 + crewCount * 8, passengersCargoCount);

            // Set default values for Longitude, Latitude, and AMSL
            float longitude = float.MinValue;
            float latitude = float.MinValue;
            float amsl = float.MinValue;

            return new Flight(code, id, originID, targetID, takeOffTime, landingTime, longitude, latitude, amsl,
                planeID, crewID, loadID);
        }

        private ulong[] ParseStringToUInt64Array(string str)
        {
            return str.Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
        }

        private ulong[] ParseByteArrayToUInt64Array(byte[] bytes, int startIndex, int count)
        {
            ulong[] result = new ulong[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = BitConverter.ToUInt64(bytes, startIndex + i * 8);
            }

            return result;
        }
    }
}