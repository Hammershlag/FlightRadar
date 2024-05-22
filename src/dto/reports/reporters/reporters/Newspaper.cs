using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.dto.reports.reporters.reporters;

public class Newspaper : NewsProvider, INewsVisitor
{
    private string lastReport;

    public Newspaper(string name) : base(name)
    {
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

    public override string Report(IReportable reportable)
    {
        reportable.Accept(this);
        return GetReport();
    }
}