using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.airports;

namespace OOD_24L_01180689.src.dto.entities.flights
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

        private DateTime lastUpdate;

        public Flight(string type, ulong id, ulong originID, ulong targetID, string takeOffTime, string landingTime,
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

            lastUpdate = DateTime.Now;
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
            var objectMap = DataStorage.GetInstance.GetIDEntityMap();
            if (objectMap.TryGetValue(TargetID, out Entity targetEntity))
            {
                Airport targetAirport = targetEntity as Airport;

                float deltaLongitude = targetAirport.Longitude - Longitude;
                float deltaLatitude = targetAirport.Latitude - Latitude;

                // Compute the  angle
                float rotation = (float)Math.Atan2(deltaLongitude, deltaLatitude);

                if (rotation < 0) rotation += 2 * (float)Math.PI;

                return rotation;
            }

            return 0;
        }


        public bool inProgress => calculateTimePassed() > 0 && calculateTimePassed() < 1;

        public void UpdateFlightPosition()
        {
            Dictionary<ulong, Entity> objectMap = DataStorage.GetInstance.GetIDEntityMap();
            DateTime currentTime = DateTime.Now;

            TimeSpan elapsedTime = currentTime - lastUpdate;
            lastUpdate = currentTime;

            if (inProgress && DataStorage.GetInstance.GetIDEntityMap().TryGetValue(TargetID, out Entity target))
            {
                Airport targetAirport = target as Airport;

                float deltaLongitude = (targetAirport.Longitude - Longitude) / calculateTimePassed() * (float)elapsedTime.TotalHours;
                float deltaLatitude = (targetAirport.Latitude - Latitude) / calculateTimePassed() * (float)elapsedTime.TotalHours;
                float deltaAMSL = (targetAirport.AMSL - AMSL) / calculateTimePassed() * (float)elapsedTime.TotalHours;

                Longitude += deltaLongitude;
                Latitude += deltaLatitude;
                AMSL += deltaAMSL;
            }
            else if (calculateTimePassed() == 0)
            {
                if (objectMap.TryGetValue(OriginID, out Entity origin2))
                {
                    Airport sourceAirport2 = origin2 as Airport;
                    Longitude = sourceAirport2.Longitude;
                    Latitude = sourceAirport2.Latitude;
                    AMSL = sourceAirport2.AMSL;
                }
            }
            else
            {
                if (objectMap.TryGetValue(TargetID, out Entity origin3))
                {
                    Airport sourceAirport3 = origin3 as Airport;
                    Longitude = sourceAirport3.Longitude;
                    Latitude = sourceAirport3.Latitude;
                    AMSL = sourceAirport3.AMSL;
                }
            }

            CalculateRotation();
        }


    }
}