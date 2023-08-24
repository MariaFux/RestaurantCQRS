using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Queries
{
    public class GetAllUserProfiles : IRequest<IEnumerable<UserProfile>>
    {
    }
}
