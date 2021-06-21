using API.Context;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Keys> : IRepository<Entity, Keys>
        where Context : MyContext
        where Entity : class
    {
        private readonly MyContext context;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(MyContext context)
        {
            this.context = context;
            entities = context.Set<Entity>();
        }

        public int Delete(Keys key)
        {
            var find = entities.Find(key);
            entities.Remove(find);
            var delete = context.SaveChanges();
            return delete;
        }

        public IEnumerable<Entity> Get()
        {
            var employee = entities.ToList();
            return employee;
        }

        public Entity Get(Keys key)
        {
            var employee = entities.Find(key);
            return employee;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var insert = context.SaveChanges();
            return insert;
        }

        public int Update(Entity entity, Keys key)
        {
            var data = entities.Find(key);
            if (data != null)
            {
                context.Entry(data).State = EntityState.Detached;
            }
            context.Entry(entity).State = EntityState.Modified;
            var update = context.SaveChanges();
            return update;
        }
    }
}
