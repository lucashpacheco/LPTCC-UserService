using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Commands;
using Peek.Framework.UserService.Consults;
using Peek.Framework.UserService.Domain;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<bool> Create(CreateUserCommand createUserCommand);

        Task<bool> Create(FollowCommand followCommand);

        Task<SignInResult> Login(LoginCommand loginCommand);

        Task<PagedResult<User>> Find(GetUsersRequest filters);

        Task<PagedResult<User>> Find(GetFollowedUsersRequest filters);

        Task<IdentityUser> FindIdentityUserByEmail(string email);

        Task<User> FindById(GetUserByIdRequest userId);

        Task<string> GenerateJwt(IdentityUser user);

        Task<ResponseBase<string>> RefreshToken(string authToken);

    }
}