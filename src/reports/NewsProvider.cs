using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.reports
{
    public abstract class NewsProvider
    {
        public string name;

        public NewsProvider(string name)
        {
            this.name = name;
        }

        public abstract string Report(IReportable reportable);
    }
}