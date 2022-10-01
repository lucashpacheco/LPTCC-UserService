using System.Threading.Tasks;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Consults;
using Peek.Framework.UserService.Domain;

namespace UserService.Service
{
    public interface IConsultHandler
    {
        Task<ResponseBase<User>> Get(GetUserByIdRequest createUserCommand);
        Task<ResponseBase<PagedResult<User>>> Get(GetUsersRequest getUsersRequest);
        Task<ResponseBase<PagedResult<User>>> Get(GetFollowedUsersRequest getUsersRequest);
    }
}