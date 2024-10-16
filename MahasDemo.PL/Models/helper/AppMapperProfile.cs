using AutoMapper;
//using MahasDemo.PL.Models.Employee;
using MahasDemo.DAL.Data.Model;
using MahasDemo.PL.Models.EmployeeViews;

namespace MahasDemo.PL.Models.helper
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
        }
    }
}
