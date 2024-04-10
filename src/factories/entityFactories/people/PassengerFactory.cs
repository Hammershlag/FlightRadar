﻿using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities.people;
using System.Text;

namespace OOD_24L_01180689.src.factories.entityFactories.people
{
    //"P" - FTR
    //"NPA" - networkSource
    public class PassengerFactory : EntityFactory
    {
        public override Passenger Create(params string[] args)
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

        public override Passenger Create(Message message)
        {
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 42)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            ulong id = BitConverter.ToUInt64(messageBytes, 7);
            ushort nameLength = BitConverter.ToUInt16(messageBytes, 15);
            string name = Encoding.ASCII.GetString(messageBytes, 17, nameLength);
            ulong age = BitConverter.ToUInt16(messageBytes, 17 + nameLength);
            string phone = Encoding.ASCII.GetString(messageBytes, 19 + nameLength, 12);
            ushort emailLength = BitConverter.ToUInt16(messageBytes, 31 + nameLength);
            string email = Encoding.ASCII.GetString(messageBytes, 33 + nameLength, emailLength);
            string _class = Encoding.ASCII.GetString(messageBytes, 33 + nameLength + emailLength, 1);
            ulong miles = BitConverter.ToUInt64(messageBytes, 34 + nameLength + emailLength);

            return new Passenger(code, id, name, age, phone, email, _class, miles);
        }
    }
}