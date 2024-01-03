using Application.Models;
using Application.UserProfiles.Queries;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.QueryHandlers
{
    internal class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfiles, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _dataContext;

        public GetAllUserProfilesQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfiles request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();
            result.Payload = await _dataContext.UserProfiles.ToListAsync(cancellationToken);
            return result;
        }
    }
}
