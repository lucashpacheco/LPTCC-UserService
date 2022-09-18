using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model.Commands;
using UserService.Model.Responses.Common;

namespace UserService.Service
{
    public interface ICommandHandler
    {
        Task<ResponseBase<string>> Create(CreateUserCommand createUserCommand);
        Task<ResponseBase<string>> Create(FollowCommand createUserCommand);
        Task<ResponseBase<string>> Create(LoginCommand createUserCommand);
        Task<ResponseBase<string>> RefreshToken(RefreshTokenCommand refreshTokenCommand);
    }
}