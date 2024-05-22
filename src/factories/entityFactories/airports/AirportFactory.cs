using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.airports;

namespace OOD_24L_01180689.src.factories.entityFactories.airports;

//"AI" - FTR
//"NAI" - networkSource
public class AirportFactory : EntityFactory
{
    public override Airport Create(params string[] args)
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

    public override Airport Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 35)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var nameLength = BitConverter.ToUInt16(messageBytes, 15);
        var name = Encoding.ASCII.GetString(messageBytes, 17, nameLength);
        var airportCode = Encoding.ASCII.GetString(messageBytes, 17 + nameLength, 3);
        var longitude = BitConverter.ToSingle(messageBytes, 20 + nameLength);
        var latitude = BitConverter.ToSingle(messageBytes, 24 + nameLength);
        var amsl = BitConverter.ToSingle(messageBytes, 28 + nameLength);
        var countryISO = Encoding.ASCII.GetString(messageBytes, 32 + nameLength, 3);

        return new Airport(code, id, name, airportCode, longitude, latitude, amsl, countryISO);
    }

    public override Airport Create()
    {
        return new Airport();
    }
}