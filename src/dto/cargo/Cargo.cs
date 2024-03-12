﻿using OOD_24L_01180689.src.factories;
using System.Globalization;
using NetworkSourceSimulator;
using System.Text;

namespace OOD_24L_01180689.src.dto.cargo
{
    //"CA"
    //"NCA"
    public class Cargo : Entity
    {
        public float Weight { get; protected set; }
        public string Code { get; protected set; }
        public string Description { get; protected set; }

        public Cargo(string type, UInt64 id, float weight, string code, string description) :
            base(type, id)
        {
            Weight = weight;
            Code = code;
            Description = description;
        }


        public override string ToString()
        {
            return $"Cargo: {Type} {ID} {Weight} {Code} {Description}";
        }
    }

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
            byte[] messageBytes = message.MessageBytes;

            if (messageBytes.Length < 27)
                return null;

            string code = Encoding.ASCII.GetString(messageBytes, 0, 3);
            uint messageLength = BitConverter.ToUInt32(messageBytes, 3);
            UInt64 id = BitConverter.ToUInt64(messageBytes, 7);
            float weight = BitConverter.ToSingle(messageBytes, 15);
            string cargoCode = Encoding.ASCII.GetString(messageBytes, 19, 6);
            ushort descriptionLength = BitConverter.ToUInt16(messageBytes, 25);
            string description = Encoding.ASCII.GetString(messageBytes, 27, descriptionLength);

            return new Cargo(code, id, weight, cargoCode, description);
        }
    }
}