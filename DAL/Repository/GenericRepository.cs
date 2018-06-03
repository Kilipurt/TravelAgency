using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.EF;
using System.Data.Entity;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private TourDBContext context;
        private DbSet<T> dbSet;

        public GenericRepository(TourDBContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Create(T obj)
        {
            dbSet.Add(obj);
        }

        public T FindById(int id)
        {
            return dbSet.Find(id);
        }

        public List<T> Get()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public void Remove(T obj)
        {
            dbSet.Remove(obj);
        }

        public void RemoveFirst()
        {
            dbSet.Remove(dbSet.FirstOrDefault());
        }

        public void Update(T obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
