namespace OOD_24L_01180689.src.dataStorage
{
    public class DataStorage
    {
        private static readonly object lockObject = new object();
        private static DataStorage instance;
        private List<object> objectList;

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
                objectList.Add(obj);
            }
        }

        public List<object> Get()
        {
            lock (lockObject)
            {
                return new List<object>(objectList);
            }
        }

        public int Count()
        {
            lock (lockObject)
            {
                return objectList.Count;
            }
        }
    }
}