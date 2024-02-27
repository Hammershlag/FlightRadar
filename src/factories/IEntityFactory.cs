using System;
using System.Globalization;
using System.Linq;
using OOD_24L_01180689.src.dto;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;

namespace OOD_24L_01180689.src.factories
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(params string[] args);
    }

    public class CargoPlaneFactory : IEntityFactory<CargoPlane>
    {
        public CargoPlane Create(params string[] args)
        {
            if (args.Length != 6) return null;
            return new CargoPlane(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                args[4],
                Convert.ToSingle(args[5])
            );
        }
    }

    public class PassengerPlaneFactory : IEntityFactory<PassengerPlane>
    {
        public PassengerPlane Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new PassengerPlane(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                args[4],
                Convert.ToUInt16(args[5]),
                Convert.ToUInt16(args[6]),
                Convert.ToUInt16(args[7])
            );
        }
    }

    public class AirportFactory : IEntityFactory<Airport>
    {
        public Airport Create(params string[] args)
        {
            if (args.Length != 8) return null;
            var culture = CultureInfo.InvariantCulture;
            return new Airport(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                args[3],
                Convert.ToSingle(args[4], culture),
                Convert.ToSingle(args[5], culture),
                Convert.ToSingle(args[6], culture),
                args[7]
            );
        }
    }

    public class CargoFactory : IEntityFactory<Cargo>
    {
        public Cargo Create(params string[] args)
        {
            if (args.Length != 5) return null;
            var culture = CultureInfo.InvariantCulture;

            return new Cargo(
                args[0],
                Convert.ToUInt64(args[1]),
                Convert.ToSingle(args[2], culture),
                args[3],
                args[4]
            );
        }
    }

    public class FlightFactory : IEntityFactory<Flight>
    {
        public Flight Create(params string[] args)
        {
            if (args.Length != 12) return null;
            var culture = CultureInfo.InvariantCulture;

            return new Flight(
                args[0],
                Convert.ToUInt64(args[1]),
                Convert.ToUInt64(args[2]),
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                Convert.ToSingle(args[6], culture),
                Convert.ToSingle(args[7], culture),
                Convert.ToSingle(args[8], culture),
                Convert.ToUInt64(args[9]),
                ParseStringToUInt64Array(args[10]),
                ParseStringToUInt64Array(args[11])
            );
        }

        private ulong[] ParseStringToUInt64Array(string str)
        {
            str = str.Trim('[', ']');
            return str.Split(';').Select(ulong.Parse).ToArray();
        }
    }

    public class CrewFactory : IEntityFactory<Crew>
    {
        public Crew Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new Crew(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                Convert.ToUInt16(args[6]),
                args[7]
            );
        }
    }

    public class PassengerFactory : IEntityFactory<Passenger>
    {
        public Passenger Create(params string[] args)
        {
            if (args.Length != 8) return null;
            return new Passenger(
                args[0],
                Convert.ToUInt64(args[1]),
                args[2],
                Convert.ToUInt64(args[3]),
                args[4],
                args[5],
                args[6],
                Convert.ToUInt64(args[7])
            );
        }
    }
}
