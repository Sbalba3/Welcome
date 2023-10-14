using AutoMapper;
using Demo.DAl.Models;
using Welcome.ViewModels;

namespace Welcome.MappingProfil
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            
        }
    }
}
