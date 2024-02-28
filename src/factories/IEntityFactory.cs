using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using OOD_24L_01180689.src.dto;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;

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