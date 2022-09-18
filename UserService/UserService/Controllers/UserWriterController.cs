using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Model.Commands;
using UserService.Model.Errors;
using UserService.Model.Responses.Common;
using UserService.Service;

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
        [ProducesResponseType(200, Type = typeof(ResponseBase<string>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<GenericError>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedError>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbiddenError>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundError>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ResponseBase<string>> CreateUser(CreateUserCommand createUserCommand)
        {
            var user = await _createCommandHandler.Create(createUserCommand);

           return user;
        }

        [HttpPost]
        [Route("follow", Name = "Follow")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<string>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<GenericError>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedError>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbiddenError>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundError>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ResponseBase<string>> Follow(FollowCommand followCommand)
        {
            var user = await _createCommandHandler.Create(followCommand);

            return user;
        }

        [HttpPost]
        [Route("login", Name = "Login")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<string>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<GenericError>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedError>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbiddenError>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundError>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ResponseBase<string>> Login(LoginCommand loginCommand)
        {
            var login = await _createCommandHandler.Create(loginCommand);

            return login;
        }

        [HttpPost]
        [Route("refreshtoken", Name = "RefreshToken")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<string>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<GenericError>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedError>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbiddenError>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundError>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ResponseBase<string>> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            var login = await _createCommandHandler.RefreshToken(refreshTokenCommand);

            return login;
        }
    }
}
