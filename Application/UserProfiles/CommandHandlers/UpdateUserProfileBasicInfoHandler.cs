using Application.UserProfiles.Commands;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.CommandHandlers
{
    internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo>
    {
        private readonly DataContext _dataContext;

        public UpdateUserProfileBasicInfoHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Unit> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var userProfile = await _dataContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.Email, request.Phone, request.DateOfBirth, request.CurrentCity);

            userProfile.UpdateBasicInfo(basicInfo);

            _dataContext.UserProfiles.Update(userProfile);
            await _dataContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
