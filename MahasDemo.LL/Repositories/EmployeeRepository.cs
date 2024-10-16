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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {
        }

        public IEnumerable<Employee> GetEmployeeByAddress(string address)
        {
            return _context.Employees.Where(e=>e.Address == address).ToList();
        }

        public IQueryable<Employee> GetEmployeeByName(string name)
        {
            return _context.Employees.Where(n => n.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
        }
    }
}
