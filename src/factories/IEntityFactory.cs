using OOD_24L_01180689.src.dto;

namespace OOD_24L_01180689.src.factories
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
    }
}