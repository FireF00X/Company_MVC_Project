using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.DAL.Data.Model
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male =1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public Department Department { get; set; }
        public int? DepartmentId { get; set; }
        public string? ImageName { get; set; }
    }
}

