using System.Threading.Tasks;
using UserService.Model.Consults;
using UserService.Model.Domain;
using UserService.Model.Responses.Common;

namespace UserService.Service
{
    public interface IConsultHandler
    {
        Task<ResponseBase<User>> Get(GetUserByIdRequest createUserCommand);
        Task<ResponseBase<PagedResult<User>>> Get(GetUsersRequest getUsersRequest);
        Task<ResponseBase<PagedResult<User>>> Get(GetFollowedUsersRequest getUsersRequest);
    }
}