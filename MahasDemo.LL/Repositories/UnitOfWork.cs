using MahasDemo.DAL.Contexts;
using MahasDemo.LL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.LL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
        }
        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
