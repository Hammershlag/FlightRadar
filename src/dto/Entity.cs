namespace OOD_24L_01180689.src.dto
{
    public abstract class Entity
    {
        public string Type { get; protected set; }
        public UInt64 ID { get; protected set; }

        protected Entity(string type, UInt64 ID)
        {
            this.ID = ID;
            Type = type;
        }

        public override string ToString()
        {
            return $"Entity: {Type} {ID}";
        }
    }
}