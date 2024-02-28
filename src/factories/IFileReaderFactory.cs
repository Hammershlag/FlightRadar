using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.readers;

namespace OOD_24L_01180689.src.factories
{
    public interface IFileReaderFactory<T> where T : IDataSource
    {
        T Create();
    }

    public abstract class FileReaderFactory : IFileReaderFactory<IDataSource>
    {
        public virtual IDataSource Create()
        {
            throw new NotImplementedException();
        }
    }
}