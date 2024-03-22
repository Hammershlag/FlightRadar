using FlightTrackerGUI;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.converters;

namespace OOD_24L_01180689.src.visualization
{
    public class FlightsGUIDataImplementation : FlightsGUIData
    {
        private List<FlightGUI> flightsData;

        public FlightsGUIDataImplementation(List<FlightGUI> flightsData)
        {
            this.flightsData = flightsData;
        }

        public static void updateFlights()
        {
            List<Flight> flightList = DataStorage.Instance.GetFlights();
            List<FlightGUI> flightGUIs = new List<FlightGUI>();
            Dictionary<ulong, Flight> closestToTakeoffFlights = new Dictionary<ulong, Flight>();
            HashSet<ulong> processedPlanes = new HashSet<ulong>();

            foreach (var flight in flightList)
            {
                float time = flight.calculateTimePassed();
                bool isFlightInProgress = time > 0 && time < 1;
                bool isFlightYetToStart = time <= 0;

                if (isFlightInProgress)
                {
                    if (!processedPlanes.Contains(flight.PlaneID))
                    {
                        processedPlanes.Add(flight.PlaneID);
                        flight.UpdateFlightPosition(true);
                        FlightGUI flightGUI = FlightToFlightGUIConverter.Convert(flight);
                        flightGUIs.Add(flightGUI);
                    }
                }
                else if (isFlightYetToStart && !processedPlanes.Contains(flight.PlaneID))
                {
                    DateTime takeoffTime = DateTime.Parse(flight.TakeOffTime);
                    if (!closestToTakeoffFlights.ContainsKey(flight.PlaneID) || DateTime.Parse(closestToTakeoffFlights[flight.PlaneID].TakeOffTime) > takeoffTime)
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
                    flight.UpdateFlightPosition(false);
                    FlightGUI flightGUI = FlightToFlightGUIConverter.Convert(flight);
                    flightGUIs.Add(flightGUI);
                }
            }

            FlightsGUIData flightsGUIData = new FlightsGUIDataImplementation(flightGUIs);
            Runner.UpdateGUI(flightsGUIData);

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
                return flightsData[index].WorldPosition;
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
                return flightsData[index].MapCoordRotation;
            }
            else
            {
                return 0;
            }
        }
    }
}
