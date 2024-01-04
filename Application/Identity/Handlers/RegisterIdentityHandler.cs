using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using Dal;
using Domain.Aggregates.UserProfileAggregate;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Identity.Handlers
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<string>>
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;

        public RegisterIdentityHandler(DataContext dataContext, UserManager<IdentityUser> userManager,
            IdentityService identityService)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();

            try
            {
                await ValidateIdentityDoesNotExsist(result, request);
                if (result.IsError) return result;

                await using var transaction = await _dataContext.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(result, request, transaction, cancellationToken);    
                if (result.IsError) return result;

                var profile = await CreateUserProfileAsync(result, request, transaction, identity, cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                result.Payload = GetJwtString(identity, profile);
            }
            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(ErrorCode.ValidationError, e));
            }
            catch (Exception ex)
            {
                result.AddUnknowError(ex.Message);
            }

            return result;
        }

        private async Task ValidateIdentityDoesNotExsist(OperationResult<string> result, RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.UserName);

            if (existingIdentity != null)
                result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(OperationResult<string> result, RegisterIdentity request, 
            IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser
            {
                Email = request.UserName,
                UserName = request.UserName
            };

            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var identityError in createdIdentity.Errors)
                {
                    result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }

            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(OperationResult<string> result, RegisterIdentity request,
            IDbContextTransaction transaction, IdentityUser identity, CancellationToken cancellationToken)
        {
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                        request.UserName, request.Phone, request.DateOfBirth, request.CurrentCity);

                var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
                _dataContext.UserProfiles.Add(profile);
                await _dataContext.SaveChangesAsync(cancellationToken);
                return profile;
            } 
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                    new Claim("IdentityId", identity.Id),
                    new Claim("UserProfileId", profile.UserProfileId.ToString())
                });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}
