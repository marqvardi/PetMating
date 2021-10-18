using AutoMapper;
using PetMating.Api.DTOs.User;
using PetMating.Api.Models;

namespace PetMating.Api.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<User, UserDetailsToReturnDto>().ReverseMap();
        }

    }
}