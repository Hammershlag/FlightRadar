using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities.flights;

namespace OOD_24L_01180689.src.visualization;

public class FlightsGUIDataAdapter : FlightsGUIData
{
    public static int displayFlightsFlag = 1;
    private List<Flight> flightsData = new();

    public void UpdateFlights()
    {
        var flightList = DataStorage.GetInstance.GetFlights();
        var flightGUIs = new List<Flight>();
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
                if (flight.inProgress)
                {
                    flight.UpdateFlightPosition();
                    flightGUIs.Add(flight);
                }
        }
        else if
            (displayFlightsFlag ==
             2) //All planes apear at most one time (there are only 14 planes, so only 14 should be visible)
        {
            var closestToTakeoffFlights = new Dictionary<ulong, Flight>();
            var processedPlanes = new HashSet<ulong>();

            foreach (var flight in flightList)
            {
                var time = flight.calculateTimePassed();
                var isFlightInProgress = flight.inProgress;
                var isFlightYetToStart = time <= 0;

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
                    var takeoffTime = DateTime.Parse(flight.TakeOffTime);
                    if (!closestToTakeoffFlights.ContainsKey(flight.PlaneID) ||
                        DateTime.Parse(closestToTakeoffFlights[flight.PlaneID].TakeOffTime) > takeoffTime)
                        closestToTakeoffFlights[flight.PlaneID] = flight;
                }
            }

            foreach (var entry in closestToTakeoffFlights)
                if (!processedPlanes.Contains(entry.Key))
                {
                    var flight = entry.Value;
                    flight.UpdateFlightPosition();
                    flightGUIs.Add(flight);
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
            return flightsData[index].ID;
        return 0;
    }

    public override WorldPosition GetPosition(int index)
    {
        if (index >= 0 && index < flightsData.Count)
            return new WorldPosition(flightsData[index].Latitude, flightsData[index].Longitude);
        return new WorldPosition();
    }

    public override double GetRotation(int index)
    {
        if (index >= 0 && index < flightsData.Count)
            return flightsData[index].CalculateRotation();
        return 0;
    }
}