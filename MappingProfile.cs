using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TMS_Api.DBModels;
using TMS_Api.DTOs;
namespace TMS_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<UserForRegistrationDto, IdentityUser>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<TruckType, TruckTypeDto>().ReverseMap();
        }
    }
}
