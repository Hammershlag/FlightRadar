using FlightTrackerGUI;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto;
// Additional using directives as needed

public class FlightToGUIAdapter : FlightGUI
{
    private Flight flight;

    public FlightToGUIAdapter(Flight flight)
    {
        this.flight = flight;
    }

    // Assuming FlightGUI has a property ID of type ulong
    public ulong ID => flight.ID;

    // Assuming FlightGUI has a property WorldPosition
    // You might need to adapt the WorldPosition from Flight's properties
    public WorldPosition WorldPosition
    {
        get
        {
            return new WorldPosition(flight.Latitude, flight.Longitude);
        }
    }

    // Assuming FlightGUI has a property MapCoordRotation of type double
    public double MapCoordRotation
    {
        get
        {
            return flight.CalculateRotation();
        }
    }

    // Implement other members of FlightGUI as needed, adapting from Flight's interface
}