using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.cargo;

namespace OOD_24L_01180689.src.factories.entityFactories.cargo;

//"CA" - FTR
//"NCA" - networkSource
public class CargoFactory : EntityFactory
{
    public override Cargo Create(params string[] args)
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

    public override Cargo Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 27)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var weight = BitConverter.ToSingle(messageBytes, 15);
        var cargoCode = Encoding.ASCII.GetString(messageBytes, 19, 6);
        var descriptionLength = BitConverter.ToUInt16(messageBytes, 25);
        var description = Encoding.ASCII.GetString(messageBytes, 27, descriptionLength);

        return new Cargo(code, id, weight, cargoCode, description);
    }

    public override Cargo Create()
    {
        return new Cargo();
    }
}