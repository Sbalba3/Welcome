using Demo.DAl.Models;
using Welcome.ViewModels;
using AutoMapper;


namespace Welcome.Mapping_Profils
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();

        }
    }
}
