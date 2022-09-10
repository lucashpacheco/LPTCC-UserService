using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model.Commands;

namespace UserService.Service
{
    public interface ICommandHandler
    {
        Task<string> Create(CreateUserCommand createUserCommand);
        Task<string> Login(LoginCommand createUserCommand);
        Task<string> RefreshToken(RefreshTokenCommand refreshTokenCommand);
    }
}