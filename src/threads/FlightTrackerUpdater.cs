using FlightTrackerGUI;
using OOD_24L_01180689.src.visualization;

namespace OOD_24L_01180689.src.threads
{
    public class FlightTrackerUpdater
    {
        private static FlightTrackerUpdater instance;
        private static readonly object lockObject = new object();

        private Thread guiThread;
        private Thread updateThread;
        private volatile bool running = true;

        private FlightTrackerUpdater()
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

        public static FlightTrackerUpdater GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new FlightTrackerUpdater();
                    }
                }
            }

            return instance;
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
            if (!guiThread.IsAlive && !updateThread.IsAlive)
            {
                guiThread.Start();
                updateThread.Start();
            }
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