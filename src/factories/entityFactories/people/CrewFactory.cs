using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.people;

namespace OOD_24L_01180689.src.factories.entityFactories.people;

//"C" - FTR
//"NCR" - networkSource
public class CrewFactory : EntityFactory
{
    public override Crew Create(params string[] args)
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

    public override Crew Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 36)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var nameLength = BitConverter.ToUInt16(messageBytes, 15);
        var name = Encoding.ASCII.GetString(messageBytes, 17, nameLength);
        ulong age = BitConverter.ToUInt16(messageBytes, 17 + nameLength);
        var phone = Encoding.ASCII.GetString(messageBytes, 19 + nameLength, 12);
        var emailLength = BitConverter.ToUInt16(messageBytes, 31 + nameLength);
        var email = Encoding.ASCII.GetString(messageBytes, 33 + nameLength, emailLength);
        var practice = BitConverter.ToUInt16(messageBytes, 33 + nameLength + emailLength);
        var role = Encoding.ASCII.GetString(messageBytes, 35 + nameLength + emailLength, 1);

        return new Crew(code, id, name, age, phone, email, practice, role);
    }

    public override Crew Create()
    {
        return new Crew();
    }
}