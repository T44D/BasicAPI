using System.Collections.Generic;

namespace API.Repository.Interfaces
{
    public interface IRepository<Entity, Key>
        where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key key);
        int Insert(Entity entity);
        int Delete(Key key);
        int Update(Entity entity, Key key);
    }
}
