using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.factories.entityFactories
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(params string[] args);
    }

    public class EntityFactory : IEntityFactory<Entity>
    {
        public virtual Entity Create(params string[] args)
        {
            throw new NotImplementedException();
        }

        public virtual Entity Create(Message message)
        {
            throw new NotImplementedException();
        }

        public virtual Entity Create()
        {
            throw new NotImplementedException();
        }
    }
}