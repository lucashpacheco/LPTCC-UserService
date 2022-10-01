using System.Collections.Generic;
using System.Threading.Tasks;
using Peek.Framework.Common.Errors;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Commands;
using UserService.Repository;

namespace UserService.Service
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IUserRepository userRepository;

        public CommandHandler(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public async Task<ResponseBase<string>> Create(CreateUserCommand createUserCommand)
        {
            var response = new ResponseBase<string>(success: false, errors: new List<string>(), data: null);

            var created = await userRepository.Create(createUserCommand);

            var identityUser = await userRepository.FindIdentityUserByEmail(createUserCommand.Email);
            if (created)
            {
                response.Success = true;
                response.Data = await userRepository.GenerateJwt(identityUser);
                return response;
            }

            var error = new UnauthorizedError();
            response.Errors.Add($"{error.Code}:{error.Message.ToString()}");

            return response;

        }

        public async Task<ResponseBase<string>> Create(FollowCommand followCommand)
        {
            var response = new ResponseBase<string>(success: false, errors: new List<string>(), data: null);

            var created = await userRepository.Create(followCommand);

            if (created)
            {
                response.Success = true;
                response.Data = "";
                return response;
            }

            var error = new UnauthorizedError();
            response.Errors.Add($"{error.Code}:{error.Message.ToString()}");

            return response;

        }

        public async Task<ResponseBase<string>> Create(LoginCommand loginCommand)
        {
            var response = new ResponseBase<string>(success: true, errors: new List<string>(), data: null);

            var logged = await userRepository.Login(loginCommand);

            if (logged.Succeeded)
            {
                response.Data = await userRepository.GenerateJwt(await userRepository.FindIdentityUserByEmail(loginCommand.Email));
                return response;
            }

            var error = new UnauthorizedError();
            response.Errors.Add($"{error.Code}:{error.Message.ToString()}");

            return response;
        }

        public async Task<ResponseBase<string>> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            var response = new ResponseBase<string>(success: true, errors: new List<string>(), data: null);

            var refreshedToken = await userRepository.RefreshToken(refreshTokenCommand.Token);

            if (!string.IsNullOrEmpty(refreshedToken.Data))
            {
                response.Data = refreshedToken.Data;
                return response;
            }

            response.Errors = refreshedToken.Errors;
            return response;
        }
    }
}
