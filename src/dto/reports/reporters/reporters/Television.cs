using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.dto.reports.reporters.reporters
{
    public class Television : NewsProvider, INewsVisitor
    {
        private string lastReport;

        public Television(string name) : base(name)
        {
        }

        public override string Report(IReportable reportable)
        {
            reportable.Accept(this);
            return GetReport();
        }

        public void Visit(Airport airport)
        {
            lastReport = $"<An image of {airport.Name} airport>";
        }

        public void Visit(CargoPlane cargoPlane)
        {
            lastReport = $"<An image of {cargoPlane.Serial} cargo plane>";
        }

        public void Visit(PassengerPlane passengerPlane)
        {
            lastReport = $"<An image of {passengerPlane.Serial} passenger plane>";
        }

        public string GetReport()
        {
            return lastReport;
        }
    }
}