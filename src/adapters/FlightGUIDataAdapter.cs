using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities.flights;

namespace OOD_24L_01180689.src.visualization
{
    public class FlightsGUIDataAdapter : FlightsGUIData
    {
        private List<Flight> flightsData = new List<Flight>();
        public static int displayFlightsFlag = 2;

        public void UpdateFlights()
        {
            List<Flight> flightList = DataStorage.GetInstance.GetFlights();
            List<Flight> flightGUIs = new List<Flight>();
            if (displayFlightsFlag == 0) //All flights appear
            {
                foreach (var flight in flightList)
                {
                    flight.UpdateFlightPosition();
                    flightGUIs.Add(flight);
                }
            }
            else if (displayFlightsFlag == 1) //Only flights that are in progress appear
            {
                foreach (var flight in flightList)
                {
                    if (flight.inProgress)
                    {
                        flight.UpdateFlightPosition();
                        flightGUIs.Add(flight);
                    }
                }
            }
            else if
                (displayFlightsFlag ==
                 2) //All planes apear at most one time (there are only 14 planes, so only 14 should be visible)
            {
                Dictionary<ulong, Flight> closestToTakeoffFlights = new Dictionary<ulong, Flight>();
                HashSet<ulong> processedPlanes = new HashSet<ulong>();

                foreach (var flight in flightList)
                {
                    float time = flight.calculateTimePassed();
                    bool isFlightInProgress = flight.inProgress;
                    bool isFlightYetToStart = time <= 0;

                    if (isFlightInProgress)
                    {
                        if (!processedPlanes.Contains(flight.PlaneID))
                        {
                            processedPlanes.Add(flight.PlaneID);
                            flight.UpdateFlightPosition();
                            flightGUIs.Add(flight);
                        }
                    }
                    else if (isFlightYetToStart && !processedPlanes.Contains(flight.PlaneID))
                    {
                        DateTime takeoffTime = DateTime.Parse(flight.TakeOffTime);
                        if (!closestToTakeoffFlights.ContainsKey(flight.PlaneID) ||
                            DateTime.Parse(closestToTakeoffFlights[flight.PlaneID].TakeOffTime) > takeoffTime)
                        {
                            closestToTakeoffFlights[flight.PlaneID] = flight;
                        }
                    }
                }

                foreach (var entry in closestToTakeoffFlights)
                {
                    if (!processedPlanes.Contains(entry.Key))
                    {
                        var flight = entry.Value;
                        flight.UpdateFlightPosition();
                        flightGUIs.Add(flight);
                    }
                }
            }

            flightsData = flightGUIs;
        }

        public override int GetFlightsCount()
        {
            return flightsData.Count;
        }

        public override ulong GetID(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return flightsData[index].ID;
            }
            else
            {
                return 0;
            }
        }

        public override WorldPosition GetPosition(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return new WorldPosition(flightsData[index].Latitude, flightsData[index].Longitude);
            }
            else
            {
                return new WorldPosition();
            }
        }

        public override double GetRotation(int index)
        {
            if (index >= 0 && index < flightsData.Count)
            {
                return flightsData[index].CalculateRotation();
            }
            else
            {
                return 0;
            }
        }
    }
}