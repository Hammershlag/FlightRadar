using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.planes;

namespace OOD_24L_01180689.src.reports.reporters
{
    public class Newspaper : NewsProvider, INewsVisitor
    {
        private string lastReport;

        public Newspaper(string name) : base(name)
        {
        }

        public override string Report(IReportable reportable)
        {
            reportable.Accept(this);
            return GetReport();
        }

        public void Visit(Airport airport)
        {
            lastReport = $"{name} - A report from the {airport.Name} airport, {airport.CountryISO}.";
        }

        public void Visit(CargoPlane cargoPlane)
        {
            lastReport = $"{name} - An interview with the crew of {cargoPlane.Serial}.";
        }

        public void Visit(PassengerPlane passengerPlane)
        {
            lastReport =
                $"{name} - Breaking news! {passengerPlane.Serial} aircraft loses EASA certification after inspection of {passengerPlane.Serial}.";
        }

        public string GetReport()
        {
            return lastReport;
        }
    }
}