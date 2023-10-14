using Microsoft.AspNetCore.Identity;
using Welcome.ViewModels;
using AutoMapper;


namespace Welcome.Mapping_Profils
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap< RoleViewModel, IdentityRole>().ForMember(d=>d.Name,o=>o.MapFrom(s=>s.RoleName)).ReverseMap();


        }

    }
}
