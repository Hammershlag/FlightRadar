using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.planes;

namespace OOD_24L_01180689.src.factories.entityFactories.planes;

//"CP" - FTR
//"NCP" - networkSource
public class CargoPlaneFactory : EntityFactory
{
    public override CargoPlane Create(params string[] args)
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

    public override CargoPlane Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 34)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var serial = Encoding.ASCII.GetString(messageBytes, 15, 10).TrimEnd('\0');
        var countryISO = Encoding.ASCII.GetString(messageBytes, 25, 3);
        var modelLength = BitConverter.ToUInt16(messageBytes, 28);
        var model = Encoding.ASCII.GetString(messageBytes, 30, modelLength);
        var maxLoad = BitConverter.ToSingle(messageBytes, 30 + modelLength);

        return new CargoPlane(code, id, serial, countryISO, model, maxLoad);
    }

    public override CargoPlane Create()
    {
        return new CargoPlane();
    }
}