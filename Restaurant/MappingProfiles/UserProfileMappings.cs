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
        }
    }
}
