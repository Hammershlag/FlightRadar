using System;
using System.Threading;
using FlightTrackerGUI;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.visualization;

namespace OOD_24L_01180689.src.threads
{
    public class FlightTrackerUpdater
    {
        private Thread guiThread;
        private Thread updateThread;
        private volatile bool running = true;

        public FlightTrackerUpdater()
        {
            guiThread = new Thread(InitializeGUI)
            {
                IsBackground = true
            };
            updateThread = new Thread(UpdateDataLoop)
            {
                IsBackground = true
            };
        }

        private void InitializeGUI()
        {
            try
            {
                Runner.Run();
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("GUI Thread was interrupted.");
            }
        }

        private void UpdateDataLoop()
        {
            var flightsGUIData = new FlightsGUIDataImplementation();

            while (running)
            {
                flightsGUIData.UpdateFlights();
                Runner.UpdateGUI(flightsGUIData);
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Update Thread was interrupted.");
                }
            }
        }

        public void Start()
        {
            guiThread.Start();
            updateThread.Start();
        }

        public void Stop()
        {
            running = false;
            if (guiThread.IsAlive)
            {
                guiThread.Interrupt();
            }
            if (updateThread.IsAlive)
            {
                updateThread.Interrupt();
            }
        }
    }
}
