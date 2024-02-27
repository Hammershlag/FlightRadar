using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dto;

namespace OOD_24L_01180689.src.readers
{
    public interface IDataSource
    {
        IEnumerable<object> ReadData(string filePath);
    }
}
