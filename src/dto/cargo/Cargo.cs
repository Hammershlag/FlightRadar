namespace OOD_24L_01180689.src.dto.cargo
{
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
}