using Application.Enums;
using Application.Models;
using Application.UserProfiles.Commands;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile, OperationResult<UserProfile>>
    {
        private readonly DataContext _dataContext;

        public DeleteUserProfileHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            var userProfile = await _dataContext.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

            if (userProfile is null)
            {
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = $"No UserProfile found with ID {request.UserProfileId}"
                };
                result.Errors.Add(error);
                return result;
            }

            _dataContext.UserProfiles.Remove(userProfile);
            await _dataContext.SaveChangesAsync(cancellationToken);

            result.Payload = userProfile;
            return result;
        }
    }
}
