using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<UserProfile>
    {
        public Guid UserProfileId { get; set; }
    }
}
