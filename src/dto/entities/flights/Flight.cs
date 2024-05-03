using Avalonia;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.airports;
using System.Text;
using OOD_24L_01180689.src.console.commands;

namespace OOD_24L_01180689.src.dto.entities.flights
{
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

        private DateTime lastUpdate;

        public Flight() : base("Wrong", ulong.MaxValue)
        {
        }
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

        protected override void InitializeFieldGetters()
        {
            fieldGetters["ID"] = () => ID;
            fieldGetters["TYPE"] = () => Type;
            fieldGetters["ORIGINID"] = () => OriginID;
            fieldGetters["TARGETID"] = () => TargetID;
            fieldGetters["TAKEOFFTIME"] = () => TakeOffTime;
            fieldGetters["LANDINGTIME"] = () => LandingTime;
            fieldGetters["LONGITUDE"] = () => Longitude;
            fieldGetters["LATITUDE"] = () => Latitude;
            fieldGetters["AMSL"] = () => AMSL;
            fieldGetters["PLANEID"] = () => PlaneID;
            fieldGetters["CREWID"] = () => string.Join(",", CrewID);
            fieldGetters["LOADID"] = () => string.Join(",", LoadID);
        }

        protected override void InitializeFieldSetters()
        {
            fieldSetters["ID"] = (value) => ID = value == null || DataStorage.GetInstance.GetIDEntityMap().TryGetValue((ulong)value, out Entity ignore) ? DataStorage.GetInstance.MaxID() + 1 : (ulong)value;
            fieldSetters["TYPE"] = (value) => Type = "FL";
            fieldSetters["ORIGINID"] = (value) => OriginID = value == null ? ulong.MaxValue : (ulong)value;
            fieldSetters["TARGETID"] = (value) => TargetID = value == null ? ulong.MaxValue : (ulong)value;
            fieldSetters["TAKEOFFTIME"] = (value) => TakeOffTime = value == null ? "Uninitialized" : ((DateTime)value).ToString("HH:mm");
            fieldSetters["LANDINGTIME"] = (value) => LandingTime = value == null ? "Uninitialized" : ((DateTime)value).ToString("HH:mm");
            fieldSetters["LONGITUDE"] = (value) => Longitude = value == null ? float.MaxValue : (float)value;
            fieldSetters["LATITUDE"] = (value) => Latitude = value == null ? float.MaxValue : (float)value;
            fieldSetters["AMSL"] = (value) => AMSL = value == null ? float.MaxValue : (float)value;
            fieldSetters["PLANEID"] = (value) => PlaneID = value == null ? ulong.MaxValue : (ulong)value;
            fieldSetters["CREWID"] = (value) => CrewID = value == null ? new ulong[0] : ((string)value).Split(',').Select(ulong.Parse).ToArray();
            fieldSetters["LOADID"] = (value) => LoadID = value == null ? new ulong[0] : ((string)value).Split(',').Select(ulong.Parse).ToArray();
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

                float deltaLongitude = (targetAirport.Longitude - Longitude) / calculateTimePassed() *
                                       (float)elapsedTime.TotalHours;
                float deltaLatitude = (targetAirport.Latitude - Latitude) / calculateTimePassed() *
                                      (float)elapsedTime.TotalHours;
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