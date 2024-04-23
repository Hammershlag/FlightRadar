using DynamicData;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.cargo;
using OOD_24L_01180689.src.dto.entities.flights;
using OOD_24L_01180689.src.dto.entities.people;
using OOD_24L_01180689.src.dto.reports.reporters;
using OOD_24L_01180689.src.dto.reports.reporters.reporters;
using OOD_24L_01180689.src.observers;

namespace OOD_24L_01180689.src.dataStorage
{
    public class DataStorage : IObserver
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

        public void Remove(object obj)
        {
            lock (lockObject)
            {
                if (obj as Entity != null)
                    iDEntityMap.Remove(((Entity)obj).getID());
                if (obj as Flight != null)
                    flightList.Remove((Flight)obj);
                if (obj as IReportable != null)
                    reporters.Remove((IReportable)obj);
                objectList.Remove(obj);
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

        public void Update(IDUpdateArgs e)
        {
            var obj = GetIDEntityMap();
            if (obj.TryGetValue(e.ObjectID, out Entity ent) && !obj.TryGetValue(e.NewObjectID, out Entity hopeThisIsNull))
            {
                Remove(ent);
                ent.ID = e.NewObjectID;
                Add(ent);
                if (ent as Person != null || ent as Cargo != null)
                {
                    for (int i = 0; i < GetFlights().Count; i++)
                    {
                        if (GetFlights()[i].CrewID.Contains(e.ObjectID) && ent as Person != null)
                        {
                            GetFlights()[i].CrewID.Replace(e.ObjectID, e.NewObjectID);
                        }
                        if (GetFlights()[i].LoadID.Contains(e.ObjectID) && ent as Cargo != null)
                        {
                            GetFlights()[i].LoadID.Replace(e.ObjectID, e.NewObjectID);
                        }
                    }
                }
            }
            
        }

        public void Update(PositionUpdateArgs e)
        {
            if (GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Flight != null)
                {
                    lock (lockObject)
                    {
                        Flight flight = (Flight)ent;
                        flight.Latitude = e.Latitude;
                        flight.Longitude = e.Longitude;
                        flight.AMSL = e.AMSL;
                    }
                }
            }
            
        }

        public void Update(ContactInfoUpdateArgs e)
        {
            
            if (GetIDEntityMap().TryGetValue(e.ObjectID, out Entity ent))
            {
                if (ent as Person != null)
                {
                    lock (lockObject)
                    {
                        Person person = (Person)ent;
                        person.Email = e.EmailAddress;
                        person.Phone = e.PhoneNumber;
                    }

                }
            }
        }
    }
}