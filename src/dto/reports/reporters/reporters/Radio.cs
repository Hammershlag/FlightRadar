using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.dto.reports.reporters.reporters;

public class Radio : NewsProvider, INewsVisitor
{
    private string lastReport;

    public Radio(string name) : base(name)
    {
    }

    public void Visit(Airport airport)
    {
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

    public override string Report(IReportable reportable)
    {
        reportable.Accept(this);
        return GetReport();
    }
}