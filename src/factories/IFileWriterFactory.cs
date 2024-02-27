using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.writers;

namespace OOD_24L_01180689.src.factories
{
    public interface IFileWriterFactory<T> where T : IWriter
    {
        T Create();
    }

    public class FileWriterFactory : IFileWriterFactory<IWriter>
    {
        public virtual IWriter Create()
        {
            throw new NotImplementedException();
        }
    }


    public class JSONWriterFactory : FileWriterFactory
    {
        public override IWriter Create()
        {
            return new JSONWriter();
        }
    }
}
