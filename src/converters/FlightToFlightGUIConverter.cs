using OOD_24L_01180689.src.dto.flights;

namespace OOD_24L_01180689.src.converters
{
    public class FlightToFlightGUIConverter : IConverter<Flight, FlightGUI>
    {
        public static FlightGUI Convert(Flight flight)
        {
            WorldPosition position = new WorldPosition(flight.Latitude, flight.Longitude);
            return new FlightGUI
                { ID = flight.ID, WorldPosition = position, MapCoordRotation = flight.CalculateRotation() };
        }
    }
}