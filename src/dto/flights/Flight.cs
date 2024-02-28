using OOD_24L_01180689.src.factories;
using System.Globalization;

namespace OOD_24L_01180689.src.dto.flights
{
    //"FL"
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

        private ulong[] ParseStringToUInt64Array(string str)
        {
            return str.Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
        }
    }
}