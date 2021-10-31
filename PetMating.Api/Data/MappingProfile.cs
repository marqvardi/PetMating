using System;
using AutoMapper;
using PetMating.Api.DTOs.Animal;
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
            CreateMap<User, UserForLogInDto>().ReverseMap();
            CreateMap<User, UserOutputDto>().ReverseMap();
            CreateMap<Animal, CreateAnimalDto>().ReverseMap();
            CreateMap<Animal, UpdateAnimalDto>().ReverseMap();
        }

        private object CreateAnimalDto()
        {
            throw new NotImplementedException();
        }
    }
}