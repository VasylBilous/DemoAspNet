using DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class EfRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;

        public EfRepository(DbContext con)
        {
            context = con;
            set = context.Set<TEntity>();
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public void Create(TEntity entity)
        {
            set.Add(entity);
            Save();
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
            Save();
        }

        public TEntity Find(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}
