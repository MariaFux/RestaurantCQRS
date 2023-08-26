using Application.Models;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Queries
{
    public class GetAllUserProfiles : IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
    }
}
