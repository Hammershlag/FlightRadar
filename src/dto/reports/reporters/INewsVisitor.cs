using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.dto.reports.reporters;

public interface INewsVisitor
{
    void Visit(Airport airport);
    void Visit(CargoPlane cargoPlane);
    void Visit(PassengerPlane passengerPlane);
    string GetReport();
}