using MahasDemo.DAL.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahasDemo.DAL.Contexts.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Salary)
                    .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Gender)
                    .HasConversion(
                    (gender)=>gender.ToString(),
                    (stringGender)=>(Gender)Enum.Parse(typeof(Gender),stringGender,true)
                );
        }
    }
}
