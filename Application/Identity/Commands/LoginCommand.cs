using Application.Models;
using MediatR;

namespace Application.Identity.Commands
{
    public class LoginCommand : IRequest<OperationResult<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
