using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Data.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int id);
        Task Insert(T t);
        void Update(T t);
        void Delete(int id);
        //void SaveChangesAsync();
    }
}
