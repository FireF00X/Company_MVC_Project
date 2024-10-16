using MahasDemo.DAL.Contexts;
using MahasDemo.DAL.Data.Model;
using MahasDemo.LL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.LL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context) 
        {
        }
    }
}
