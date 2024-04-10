namespace OOD_24L_01180689.src.dto.entities
{
    public abstract class Entity
    {
        public string Type { get; protected set; }
        public ulong ID { get; protected set; }

        protected Entity(string type, ulong ID)
        {
            this.ID = ID;
            Type = type;
        }

        public ulong getID()
        {
            return ID;
        }

        public override string ToString()
        {
            return $"Entity: {Type} {ID}";
        }
    }
}