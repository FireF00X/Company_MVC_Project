using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.DAL.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
        public string Fname {  get; set; }
        public string Lname { get; set; }
    }
}
