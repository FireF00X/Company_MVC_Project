using MahasDemo.DAL.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.LL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
    }
}
