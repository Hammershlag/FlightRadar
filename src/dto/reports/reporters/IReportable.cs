using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.dto.reports.reporters
{
    public interface IReportable
    {
        void Accept(INewsVisitor visitor);
    }
}