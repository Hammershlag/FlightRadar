using NetworkSourceSimulator;

namespace OOD_24L_01180689.src.serverSimulator
{
    public class ServerSimulator
    {
        private static ServerSimulator instance;
        private static readonly object lockObject = new object();

        private int minDelay = 0;
        private int maxDelay = 1;
        private string input = "";
        private NetworkSourceSimulator.NetworkSourceSimulator networkSourceInstance;
        private Thread networkSourceThread;

        public event EventHandler<NewDataReadyArgs> OnDataReady;

        private ServerSimulator(string input, int minDelay, int maxDelay)
        {
            this.input = input;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
        }

        public static ServerSimulator GetInstance(string input, int minDelay, int maxDelay)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    if (instance == null)
                    {
                        instance = new ServerSimulator(input, minDelay, maxDelay);
                    }
                }
            }

            return instance;
        }

        public void Run()
        {
            networkSourceInstance = new NetworkSourceSimulator.NetworkSourceSimulator(input, minDelay, maxDelay);
            networkSourceInstance.OnNewDataReady += (sender, e) => { OnDataReady?.Invoke(this, e); };
            networkSourceThread = StartNetworkSourceSimulator();
        }

        private Thread StartNetworkSourceSimulator()
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    networkSourceInstance.Run();
                }
                catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine("Server Interrupted");
                }
            });
            thread.Start();

            return thread;
        }

        public Message GetMessageAt(int index)
        {
            return networkSourceInstance.GetMessageAt(index);
        }

        public void Stop()
        {
            try
            {
                if (networkSourceThread != null && networkSourceThread.IsAlive)
                {
                    networkSourceThread.Interrupt();
                    networkSourceThread.Join();
                }
            }
            catch
            {
                Console.WriteLine("Server Interrupted");
            }
        }
    }
}