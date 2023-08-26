using Application.Models;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Commands
{
    public class DeleteUserProfile : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
