using Application.Enums;
using Application.Models;
using Application.UserProfiles.Commands;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.CommandHandlers
{
    internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo, OperationResult<UserProfile>>
    {
        private readonly DataContext _dataContext;

        public UpdateUserProfileBasicInfoHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _dataContext.UserProfiles
                    .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

                if(userProfile is null)
                {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, 
                        Message = $"No UserProfile found with ID {request.UserProfileId}" };
                    result.Errors.Add(error);
                    return result;
                }

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                    request.Email, request.Phone, request.DateOfBirth, request.CurrentCity);

                userProfile.UpdateBasicInfo(basicInfo);

                _dataContext.UserProfiles.Update(userProfile);
                await _dataContext.SaveChangesAsync(cancellationToken);

                result.Payload = userProfile;
            }
            catch (UserProfileNotValidException ex)
            {
                result.IsError = true;
                ex.ValidationErrors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
            }
            catch (Exception ex) 
            {
                var error = new Error { Code = ErrorCode.ServerError, Message = ex.Message };
                result.IsError = true;
                result.Errors.Add(error);
            }            

            return result;
        }
    }
}
