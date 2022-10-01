using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Consults;
using Peek.Framework.UserService.Domain;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserReaderController : ControllerBase
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
        [ProducesResponseType(200, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<ApplicationException>))]
        public Task<ResponseBase<User>> GetUser([FromRoute] GetUserByIdRequest getUserByIdRequest)
        {
            var user = _consultHandler.Get(getUserByIdRequest);
            return user;
        }

        [HttpGet]
        [Route("getusers", Name = "Get Users")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<PagedResult<User>>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<ApplicationException>))]
        public Task<ResponseBase<PagedResult<User>>> GetUsers([FromQuery] GetUsersRequest getUsersRequest)
        {
            var user = _consultHandler.Get(getUsersRequest);
            return user;
        }

        [HttpGet]
        [Route("getfollowedusers", Name = "Get Followed Users")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<PagedResult<User>>))]
        [ProducesResponseType(400, Type = typeof(ResponseBase<BadRequestResult>))]
        [ProducesResponseType(401, Type = typeof(ResponseBase<UnauthorizedResult>))]
        [ProducesResponseType(403, Type = typeof(ResponseBase<ForbidResult>))]
        [ProducesResponseType(404, Type = typeof(ResponseBase<NotFoundResult>))]
        [ProducesResponseType(500, Type = typeof(ResponseBase<ApplicationException>))]
        public Task<ResponseBase<PagedResult<User>>> GetFollowedUsers([FromQuery] GetFollowedUsersRequest getUsersRequest)
        {
            var user = _consultHandler.Get(getUsersRequest);
            return user;
        }
    }
}
