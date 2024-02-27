using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.writers
{
    public interface IWriter
    {
        void Write(IEnumerable<object> objects, string filePath);
    }
}
