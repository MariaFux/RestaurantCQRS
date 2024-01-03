using Application.Enums;
using Application.Models;
using Application.UserProfiles.Queries;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.QueryHandlers
{
    internal class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, OperationResult<UserProfile>>
    {
        private readonly DataContext _dataContext;

        public GetUserProfileByIdHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
            var userProfile = await _dataContext.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);
            
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

            result.Payload = userProfile;
            return result;
        }
    }
}
