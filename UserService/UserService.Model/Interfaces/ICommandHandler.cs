using System.Threading.Tasks;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Commands;

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