using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Model.Commands;
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
        public async Task<string> Create(CreateUserCommand createUserCommand)
        {
            var created = await userRepository.Create(createUserCommand);

            var user = await userRepository.FindByEmail(createUserCommand.Email);
            if (created.Succeeded)
                return await userRepository.GenerateJwt(user);

            return created.ToString();
        }

        public async Task<string> Login(LoginCommand loginCommand)
        {
            var logged = await userRepository.Login(loginCommand);

            if (logged.Succeeded)
                return await userRepository.GenerateJwt(await userRepository.FindByEmail(loginCommand.Email));

            return logged.ToString();    
        }
        
        public async Task<string> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            var refreshed = await userRepository.RefreshToken(refreshTokenCommand.Token);

            return refreshed;    
        }
    }
}
