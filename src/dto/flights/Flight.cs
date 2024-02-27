using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.flights
{
    //"FL"
    public class Flight : Entity
    {
        public ulong OriginID { get; set; }
        public ulong TargetID { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; set; }
        public ulong[] CrewID { get; set; }
        public ulong[] LoadID { get; set; }

        public Flight(string type, UInt64 id, ulong originID, ulong targetID, string takeOffTime, string landingTime,
            float longitude,
            float latitude, float amsl, ulong planeID, ulong[] crewID, ulong[] loadID) :
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
            return $"Flight: {Type} {ID} {OriginID} {TargetID} {TakeOffTime} {LandingTime} {Longitude} {Latitude} {AMSL} {PlaneID} {CrewID} {LoadID}";
        }
    }
}
