using MahasDemo.DAL.Contexts;
using MahasDemo.DAL.Data.Model;
using MahasDemo.LL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.LL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(e=>e.Department).AsNoTracking().ToList();
            }
            else
            {
                return _context.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetById(int id)
        {
            return _context.Find<T>(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

