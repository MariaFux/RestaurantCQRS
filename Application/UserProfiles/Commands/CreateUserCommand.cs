using Application.Models;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.Commands
{
    public class CreateUserCommand : IRequest<OperationResult<UserProfile>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
    }
}
