using MahasDemo.DAL.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.LL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeeByAddress(string address);
        IQueryable<Employee> GetEmployeeByName(string Name);
    }
}
