using OOD_24L_01180689.src.factories;
using System.Globalization;
using NetworkSourceSimulator;
using System.Text;

namespace OOD_24L_01180689.src.dto.flights
{
    //"FL"
    //"NFL"
    public class Flight : Entity
    {
        public ulong OriginID { get; protected set; }
        public ulong TargetID { get; protected set; }
        public string TakeOffTime { get; protected set; }
        public string LandingTime { get; protected set; }
        public float Longitude { get; protected set; }
        public float Latitude { get; protected set; }
        public float AMSL { get; protected set; }
        public ulong PlaneID { get; protected set; }
        public ulong[] CrewID { get; protected set; }
        public ulong[] LoadID { get; protected set; }

        public Flight(string type, UInt64 id, ulong originID, ulong targetID, string takeOffTime, string landingTime, float longitude, float latitude, float amsl, ulong planeID, ulong[] crewID, ulong[] loadID) :
            base(type, id)
        {
            OriginID = originID;
            TargetID = targetID;
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            PlaneID = planeID;
            CrewID = crewID;
            LoadID = loadID;
        }

        public override string ToString()
        {
            return
                $"Flight: {Type} {ID} {OriginID} {TargetID} {TakeOffTime} {LandingTime} {Longitude} {Latitude} {AMSL} {PlaneID} {CrewID} {LoadID}";
        }
    }

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
            UInt64 id = BitConverter.ToUInt64(messageBytes, 7);
            UInt64 originID = BitConverter.ToUInt64(messageBytes, 15);
            UInt64 targetID = BitConverter.ToUInt64(messageBytes, 23);
            string takeOffTime = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 31)).ToString("yyyy-MM-dd HH:mm:ss");
            string landingTime = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 39)).ToString("yyyy-MM-dd HH:mm:ss");
            UInt64 planeID = BitConverter.ToUInt64(messageBytes, 47);
            ushort crewCount = BitConverter.ToUInt16(messageBytes, 55);
            ulong[] crewID = ParseStringToUInt64Array(messageBytes, 57, crewCount);
            ushort passengersCargoCount = BitConverter.ToUInt16(messageBytes, 57 + crewCount * 8);
            ulong[] loadID = ParseStringToUInt64Array(messageBytes, 59 + crewCount * 8, passengersCargoCount);

            // Set default values for Longitude, Latitude, and AMSL
            float longitude = float.MinValue;
            float latitude = float.MinValue;
            float amsl = float.MinValue;

            return new Flight(code, id, originID, targetID, takeOffTime, landingTime, longitude, latitude, amsl, planeID, crewID, loadID);
        }

        private ulong[] ParseStringToUInt64Array(string str)
        {
            return str.Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
        }

        private ulong[] ParseStringToUInt64Array(byte[] bytes, int startIndex, int count)
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