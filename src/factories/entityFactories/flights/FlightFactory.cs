using System.Globalization;
using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.flights;

namespace OOD_24L_01180689.src.factories.entityFactories.flights;

//"FL" - FTR
//"NFL" - networkSource
public class FlightFactory : EntityFactory
{
    public override Flight Create(params string[] args)
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

    public override Flight Create(Message message)
    {
        var messageBytes = message.MessageBytes;

        if (messageBytes.Length < 59)
            return null;

        var code = Encoding.ASCII.GetString(messageBytes, 0, 3);
        var messageLength = BitConverter.ToUInt32(messageBytes, 3);
        var id = BitConverter.ToUInt64(messageBytes, 7);
        var originID = BitConverter.ToUInt64(messageBytes, 15);
        var targetID = BitConverter.ToUInt64(messageBytes, 23);
        var takeOffTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 31));
        var landingTimeDate = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(messageBytes, 39));

        if (landingTimeDate < takeOffTimeDate) landingTimeDate = landingTimeDate.AddDays(1);

        var takeOffTime = takeOffTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
        var landingTime = landingTimeDate.ToString("yyyy-MM-dd HH:mm:ss");
        var planeID = BitConverter.ToUInt64(messageBytes, 47);
        var crewCount = BitConverter.ToUInt16(messageBytes, 55);
        var crewID = ParseByteArrayToUInt64Array(messageBytes, 57, crewCount);
        var passengersCargoCount = BitConverter.ToUInt16(messageBytes, 57 + crewCount * 8);
        var loadID = ParseByteArrayToUInt64Array(messageBytes, 59 + crewCount * 8, passengersCargoCount);

        var longitude = float.MinValue;
        var latitude = float.MinValue;
        var amsl = float.MinValue;

        return new Flight(code, id, originID, targetID, takeOffTime, landingTime, longitude, latitude, amsl,
            planeID, crewID, loadID);
    }

    private ulong[] ParseStringToUInt64Array(string str)
    {
        return str.Trim('[', ']').Split(';').Select(ulong.Parse).ToArray();
    }

    private ulong[] ParseByteArrayToUInt64Array(byte[] bytes, int startIndex, int count)
    {
        var result = new ulong[count];
        for (var i = 0; i < count; i++) result[i] = BitConverter.ToUInt64(bytes, startIndex + i * 8);

        return result;
    }

    public override Flight Create()
    {
        return new Flight();
    }
}