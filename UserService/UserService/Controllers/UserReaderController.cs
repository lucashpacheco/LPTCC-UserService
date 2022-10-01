using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Peek.Framework.Common.Errors;
using Peek.Framework.Common.Responses;
using Peek.Framework.Common.Utils;
using Peek.Framework.UserService.Consults;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserReaderController : BaseController
    {

        private readonly ILogger<UserReaderController> _logger;
        private readonly IConsultHandler _consultHandler;

        public UserReaderController(ILogger<UserReaderController> logger, IConsultHandler consultHandler)
        {
            _logger = logger;
            _consultHandler = consultHandler;
        }

        [HttpGet]
        [Route("getuser/{UserId}", Name = "Get User")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> GetUser([FromRoute] GetUserByIdRequest getUserByIdRequest)
        {
            var user = await _consultHandler.Get(getUserByIdRequest);
            return CustomResponse(user);
        }

        [HttpGet]
        [Route("getusers", Name = "Get Users")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> GetUsers([FromQuery] GetUsersRequest getUsersRequest)
        {
            var user = await _consultHandler.Get(getUsersRequest);
            return CustomResponse(user);
        }

        [HttpGet]
        [Route("getfollowedusers", Name = "Get Followed Users")]
        [ProducesResponseType(200, Type = typeof(ActionResult))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<GenericError>))]
        public async Task<ActionResult> GetFollowedUsers([FromQuery] GetFollowedUsersRequest getUsersRequest)
        {
            var user = await _consultHandler.Get(getUsersRequest);

            return CustomResponse(user);
        }
    }
}
