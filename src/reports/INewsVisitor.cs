using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.planes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.reports
{
    public interface INewsVisitor
    {
        void Visit(Airport airport);
        void Visit(CargoPlane cargoPlane);
        void Visit(PassengerPlane passengerPlane);
        string GetReport();
    }
}