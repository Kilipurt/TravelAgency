using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T obj);
        T FindById(int id);
        List<T> Get();
        void Remove(T obj);
        void RemoveFirst();
        void Update(T obj);
    }
}
