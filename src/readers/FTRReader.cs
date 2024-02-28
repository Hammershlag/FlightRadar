using OOD_24L_01180689.src.factories;
using System;
namespace OOD_24L_01180689.src.readers
{
    public class FTRReader : Reader
    {
        public override IEnumerable<object> ReadData(string dir, string filename)
        {
            var filePath = dir + "\\" + filename;

            if (!filePath.EndsWith(".ftr"))
            {
                throw new ArgumentException("Invalid file type");
            }

            var objects = new List<object>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(',');
                    var objectType = data[0];

                    if (factoryMethods.TryGetValue(objectType, out var factoryMethod))
                    {
                        var obj = factoryMethod(data);
                        objects.Add(obj);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid object type");
                    }
                }
            }

            return objects;
        }
    }

    public class FTRReaderFactory : FileReaderFactory
    {
        public override IDataSource Create()
        {
            return new FTRReader();
        }
    }
}