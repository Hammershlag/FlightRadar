using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.observers;

namespace OOD_24L_01180689.src.logging
{
    public interface ILogger : IObserver
    {
        void Log(string message);
    }
}