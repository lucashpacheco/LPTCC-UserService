using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model.Commands;
using UserService.Model.Consults;
using UserService.Model.Domain;
using UserService.Model.Responses.Common;

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