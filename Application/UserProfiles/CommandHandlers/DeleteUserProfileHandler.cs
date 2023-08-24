using Application.UserProfiles.Commands;
using Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile>
    {
        private readonly DataContext _dataContext;

        public DeleteUserProfileHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Unit> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var userProfile = await _dataContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

            _dataContext.UserProfiles.Remove(userProfile);
            await _dataContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
