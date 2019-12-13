using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.DTO;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(destinaton => destinaton.PhotoUrl, 
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destinaton => destinaton.Age, 
                opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(destinaton => destinaton.PhotoUrl, 
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destinaton => destinaton.Age, 
                opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForRetunDto>();
            CreateMap<PhotoForCreationDto, Photo>();
        }

        public int CalculateAge(DateTime theDateTime) 
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}