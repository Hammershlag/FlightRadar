using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.airports;

namespace OOD_24L_01180689.src.dto.flights
{
    public class Flight : Entity
    {
        public ulong OriginID { get; protected set; }
        public ulong TargetID { get; protected set; }
        public string TakeOffTime { get; protected set; }
        public string LandingTime { get; protected set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public ulong PlaneID { get; protected set; }
        public ulong[] CrewID { get; protected set; }
        public ulong[] LoadID { get; protected set; }

        public Flight(string type, UInt64 id, ulong originID, ulong targetID, string takeOffTime, string landingTime,
            float longitude, float latitude, float amsl, ulong planeID, ulong[] crewID, ulong[] loadID) :
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

        public float calculateTimePassed()
        {
            DateTime takeoffTime = DateTime.Parse(TakeOffTime);
            DateTime landingTime = DateTime.Parse(LandingTime);

            TimeSpan takeoff = takeoffTime.TimeOfDay;
            TimeSpan landing = landingTime.TimeOfDay;

            TimeSpan totalDuration;
            if (landing < takeoff)
            {
                totalDuration = TimeSpan.FromHours(24) - takeoff + landing;
            }
            else
            {
                totalDuration = landing - takeoff;
            }

            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime.TimeOfDay - takeoff;

            float percentage = (float)(elapsedTime.TotalMilliseconds / totalDuration.TotalMilliseconds);

            if (percentage < 0)
            {
                percentage = 0;
            }
            else if (percentage > 1)
            {
                percentage = 1;
            }

            return percentage;
        }

        public float CalculateRotation()
        {
            var objectMap = DataStorage.Instance.GetIDEntityMap();

            if (objectMap.TryGetValue(OriginID, out Entity originEntity) && originEntity is Airport sourceAirport &&
                objectMap.TryGetValue(TargetID, out Entity targetEntity) && targetEntity is Airport targetAirport)
            {
                float deltaLongitude = targetAirport.Longitude - sourceAirport.Longitude;
                float deltaLatitude = targetAirport.Latitude - sourceAirport.Latitude;

                float rotation = (float)Math.Atan2(deltaLongitude, deltaLatitude);


                if (rotation < 0) rotation += 2 * (float)Math.PI;

                return rotation;
            }

            return 0;
        }

        public void UpdateFlightPosition(bool inProgress)
        {
            Dictionary<UInt64, Entity> objectMap = DataStorage.Instance.GetIDEntityMap();
            if (inProgress && objectMap.TryGetValue(this.TargetID, out Entity target) && target is Airport targetAirport &&
                objectMap.TryGetValue(this.OriginID, out Entity origin) && origin is Airport sourceAirport)
            {
                float deltaLongitude = targetAirport.Longitude - sourceAirport.Longitude;
                float deltaLatitude = targetAirport.Latitude - sourceAirport.Latitude;
                float deltaAMSL = targetAirport.AMSL - sourceAirport.AMSL;
                float time = this.calculateTimePassed();

                this.Longitude = sourceAirport.Longitude + deltaLongitude * time;
                this.Latitude = sourceAirport.Latitude + deltaLatitude * time;
                this.AMSL = sourceAirport.AMSL + deltaAMSL * time;
            }
            else if (!inProgress)
            {
                if (objectMap.TryGetValue(this.OriginID, out Entity origin2) && origin2 is Airport sourceAirport2)
                {
                    this.Longitude = sourceAirport2.Longitude;
                    this.Latitude = sourceAirport2.Latitude;
                    this.AMSL = sourceAirport2.AMSL;
                }
            }
        }

    }
}