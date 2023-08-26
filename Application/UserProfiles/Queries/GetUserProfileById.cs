using Application.Models;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
