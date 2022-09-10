using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model.Commands;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> Create(CreateUserCommand createUserCommand);

        Task<SignInResult> Login(LoginCommand loginCommand);

        Task<IdentityUser> FindByEmail(string email);

        Task<string> GenerateJwt(IdentityUser user);

        Task<string> RefreshToken(string authToken);

    }
}