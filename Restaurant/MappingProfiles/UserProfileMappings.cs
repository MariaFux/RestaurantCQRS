using Application.UserProfiles.Commands;
using AutoMapper;
using Domain.Aggregates.UserProfileAggregate;
using Restaurant.Contracts.Menus.Responses;
using Restaurant.Contracts.UserProfile.Requests;
using Restaurant.Contracts.UserProfile.Responses;

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
