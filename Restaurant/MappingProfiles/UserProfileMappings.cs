using Application.Contracts.Menus.Responses;
using Application.Contracts.UserProfile.Requests;
using Application.Contracts.UserProfile.Responses;
using Application.UserProfiles.Commands;
using AutoMapper;
using Domain.Aggregates.UserProfileAggregate;

namespace Restaurant.MappingProfiles
{
    public class UserProfileMappings : Profile
    {
        public UserProfileMappings()
        {
            CreateMap<UserProfileCreateUpdate, CreateUserCommand>();
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
            CreateMap<UserProfile, UserRecipe>()
                .ForMember(dest => dest.FullName, opt
                    => opt.MapFrom(src
                    => src.BasicInfo.FirstName + " " + src.BasicInfo.LastName))
                .ForMember(dest => dest.Age, opt
                    => opt.MapFrom(src => DateTime.Now.Year - src.BasicInfo.DateOfBirth.Year));
        }
    }
}
