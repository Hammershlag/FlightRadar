using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.factories.entityFactories.planes;

//"PP" - FTR
//"NPP" - networkSource
public class PassengerPlaneFactory : EntityFactory
{
    public override PassengerPlane Create(params string[] args)
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

    public override PassengerPlane Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 36)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var serial = Encoding.ASCII.GetString(messageBytes, 15, 10).TrimEnd('\0');
        var countryISO = Encoding.ASCII.GetString(messageBytes, 25, 3);
        var modelLength = BitConverter.ToUInt16(messageBytes, 28);
        var model = Encoding.ASCII.GetString(messageBytes, 30, modelLength);
        var firstClassSize = BitConverter.ToUInt16(messageBytes, 30 + modelLength);
        var businessClassSize = BitConverter.ToUInt16(messageBytes, 32 + modelLength);
        var economyClassSize = BitConverter.ToUInt16(messageBytes, 34 + modelLength);

        return new PassengerPlane(code, id, serial, countryISO, model, firstClassSize, businessClassSize,
            economyClassSize);
    }

    public override PassengerPlane Create()
    {
        return new PassengerPlane();
    }
}