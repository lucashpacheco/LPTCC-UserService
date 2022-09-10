using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Model.Commands;
using UserService.Model.Inputs;
using UserService.Model.Errors;
using UserService.Service;
using Microsoft.AspNetCore.Identity;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWriterController : ControllerBase
    {

        private readonly ILogger<UserWriterController> _logger;
        private readonly ICommandHandler _createCommandHandler;

        public UserWriterController(ILogger<UserWriterController> logger , ICommandHandler createCommandHandler)
        {
            _logger = logger;
            _createCommandHandler = createCommandHandler;
        }

        [HttpPost]
        [Route("create", Name = "CreateUser")]
        [ProducesResponseType(200, Type = typeof(NoContentResult))]
        [ProducesResponseType(400, Type = typeof(GenericError))]
        [ProducesResponseType(401, Type = typeof(GenericError))]
        [ProducesResponseType(403, Type = typeof(GenericError))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public Task<string> CreateUser(CreateUserCommand createUserCommand)
        {
            var user = _createCommandHandler.Create(createUserCommand);

           return user;
        }

        [HttpPost]
        [Route("login", Name = "Login")]
        [ProducesResponseType(200, Type = typeof(NoContentResult))]
        [ProducesResponseType(400, Type = typeof(GenericError))]
        [ProducesResponseType(401, Type = typeof(GenericError))]
        [ProducesResponseType(403, Type = typeof(GenericError))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public Task<string> Login(LoginCommand loginCommand)
        {
            var login = _createCommandHandler.Login(loginCommand);

            return login;
        }

        [HttpPost]
        [Route("refreshtoken", Name = "RefreshToken")]
        [ProducesResponseType(200, Type = typeof(NoContentResult))]
        [ProducesResponseType(400, Type = typeof(GenericError))]
        [ProducesResponseType(401, Type = typeof(GenericError))]
        [ProducesResponseType(403, Type = typeof(GenericError))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public Task<string> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            var login = _createCommandHandler.RefreshToken(refreshTokenCommand);

            return login;
        }
    }
}
