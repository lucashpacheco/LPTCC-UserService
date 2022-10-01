using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Peek.Framework.Common.Errors;
using Peek.Framework.Common.Responses;
using Peek.Framework.Common.Utils;
using Peek.Framework.UserService.Commands;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserWriterController : BaseController
    {

        private readonly ILogger<UserWriterController> _logger;
        private readonly ICommandHandler _createCommandHandler;

        public UserWriterController(ILogger<UserWriterController> logger, ICommandHandler createCommandHandler)
        {
            _logger = logger;
            _createCommandHandler = createCommandHandler;
        }

        [HttpPost]
        [Route("create", Name = "CreateUser")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> CreateUser(CreateUserCommand createUserCommand)
        {
            var user = await _createCommandHandler.Create(createUserCommand);

            return CustomResponse(user);
        }

        [HttpPost]
        [Route("follow", Name = "Follow")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> Follow(FollowCommand followCommand)
        {
            var user = await _createCommandHandler.Create(followCommand);

            return CustomResponse(user);
        }

        [HttpPost]
        [Route("login", Name = "Login")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> Login(LoginCommand loginCommand)
        {
            var login = await _createCommandHandler.Create(loginCommand);

            return CustomResponse(login);
        }

        [HttpPost]
        [Route("refreshtoken", Name = "RefreshToken")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> RefreshToken(RefreshTokenCommand refreshTokenCommand)
        {
            var login = await _createCommandHandler.RefreshToken(refreshTokenCommand);

            return CustomResponse(login);
        }
    }
}
