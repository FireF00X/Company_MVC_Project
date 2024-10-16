using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.DAL.Data.Model
{
    public  class Department : BaseModel
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is a required field")]
        public int Code { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateTime DateOfCreation { get; set; }= DateTime.Now;

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
