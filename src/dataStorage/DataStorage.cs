using FlightTrackerGUI;
using OOD_24L_01180689.src.converters;
using OOD_24L_01180689.src.dto;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.flights;

namespace OOD_24L_01180689.src.dataStorage
{
    public class DataStorage
    {
        private static readonly object lockObject = new object();
        private static DataStorage instance;
        private List<object> objectList;
        private Dictionary<UInt64, Entity> iDEntityMap = new Dictionary<ulong, Entity>();
        private List<Flight> flightList = new List<Flight>();

        private DataStorage()
        {
            objectList = new List<object>();
        }

        public static DataStorage Instance
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

        public void Add(object obj)
        {
            lock (lockObject)
            {
                if(obj is Entity entity) 
                    iDEntityMap.Add(entity.getID(), entity);
                if(obj is Flight flight)
                    flightList.Add(flight);
                objectList.Add(obj);
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
            lock(lockObject)
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