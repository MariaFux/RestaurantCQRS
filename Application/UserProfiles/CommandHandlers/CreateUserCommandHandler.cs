using Application.UserProfiles.Commands;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace Application.UserProfiles.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserProfile>
    {
        private readonly DataContext _dataContext;

        public CreateUserCommandHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserProfile> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.Email, request.Phone, request.DateOfBirth, request.CurrentCity);

            var userProfile = UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

            _dataContext.UserProfiles.Add(userProfile);
            await _dataContext.SaveChangesAsync();

            return userProfile;
        }
    }
}
