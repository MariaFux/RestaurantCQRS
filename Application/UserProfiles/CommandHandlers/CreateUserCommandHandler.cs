using Application.Models;
using Application.UserProfiles.Commands;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _dataContext;

        public CreateUserCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.Email, request.Phone, request.DateOfBirth, request.CurrentCity);

            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            _dataContext.UserProfiles.Add(userProfile);
            await _dataContext.SaveChangesAsync();

            result.Payload = userProfile;

            return result;
        }
    }
}
