using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.dto.reports.reporters.reporters
{
    public class Radio : NewsProvider, INewsVisitor
    {
        private string lastReport; // Temporary storage for the last report

        public Radio(string name) : base(name)
        {
        }

        public override string Report(IReportable reportable)
        {
            reportable.Accept(this); // This will generate the report
            return GetReport(); // Retrieve the generated report
        }

        public void Visit(Airport airport)
        {
            // Generate the report specific to Radio and Airport
            lastReport = $"Reporting for {name}, Ladies and Gentlemen, we are at the {airport.Name} airport.";
        }

        public void Visit(CargoPlane cargoPlane)
        {
            lastReport =
                $"Reporting for {name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public void Visit(PassengerPlane passengerPlane)
        {
            lastReport =
                $"Reporting for {name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} take off.";
        }

        public string GetReport()
        {
            return lastReport;
        }
    }
}