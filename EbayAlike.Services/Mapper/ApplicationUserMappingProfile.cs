using AutoMapper;
using EbayAlike.Domain.Models;
using EbayAlike.Services.Interfaces;
using EbayAlike.ViewModels;
using System;

namespace EbayAlike.Services.Mapper
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<CreateUserViewModel, ApplicationUser>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<ApplicationUser, TokenViewModel>()
             .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));
        }
    }
}
