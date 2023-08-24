using Application.UserProfiles.Commands;
using AutoMapper;
using Domain.Aggregates.UserProfileAggregate;

namespace Application.MappingProfiles
{
    internal class UserProfileMap : Profile
    {
        public UserProfileMap()
        {
            CreateMap<CreateUserCommand, BasicInfo>();
        }
    }
}
