using OOD_24L_01180689.src.dto;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.reports;

namespace OOD_24L_01180689.src.dataStorage
{
    public class DataStorage
    {
        private static readonly object lockObject = new object();
        private static DataStorage instance;
        private List<object> objectList;
        private Dictionary<UInt64, Entity> iDEntityMap = new Dictionary<ulong, Entity>();
        private List<Flight> flightList = new List<Flight>();
        private List<NewsProvider> providers = new List<NewsProvider>();
        private List<IReportable> reporters = new List<IReportable>();

        private DataStorage()
        {
            objectList = new List<object>();
        }

        public static DataStorage GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DataStorage();
                        }
                    }
                }

                return instance;
            }
        }

        public void addNewsProvider(NewsProvider provider)
        {
            lock (lockObject)
            {
                providers.Add(provider);
            }
        }

        public void Add(object obj)
        {
            lock (lockObject)
            {
                if (obj as Entity != null)
                    iDEntityMap.Add(((Entity)obj).getID(), (Entity)obj);
                if (obj as Flight != null)
                    flightList.Add((Flight)obj);
                if (obj as IReportable != null)
                    reporters.Add((IReportable)obj);
                objectList.Add(obj);
            }
        }

        public List<NewsProvider> GetNewsProviders()
        {
            lock (lockObject)
            {
                return new List<NewsProvider>(providers);
            }
        }

        public List<IReportable> GetReporters()
        {
            lock (lockObject)
            {
                return new List<IReportable>(reporters);
            }
        }

        public List<object> GetObjectList()
        {
            lock (lockObject)
            {
                return new List<object>(objectList);
            }
        }

        public Dictionary<UInt64, Entity> GetIDEntityMap()
        {
            lock (lockObject)
            {
                return new Dictionary<ulong, Entity>(iDEntityMap);
            }
        }

        public List<Flight> GetFlights()
        {
            lock (lockObject)
            {
                return new List<Flight>(flightList);
            }
        }

        public int CountObjectList()
        {
            lock (lockObject)
            {
                return objectList.Count;
            }
        }

        public int CountIDEntityMap()
        {
            lock (lockObject)
            {
                return iDEntityMap.Count;
            }
        }

        public int CountFlights()
        {
            lock (lockObject)
            {
                return flightList.Count;
            }
        }
    }
}