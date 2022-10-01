using System.Collections.Generic;
using System.Threading.Tasks;
using Peek.Framework.Common.Errors;
using Peek.Framework.Common.Responses;
using Peek.Framework.UserService.Consults;
using Peek.Framework.UserService.Domain;
using UserService.Repository;

namespace UserService.Service
{
    public class ConsultHandler : IConsultHandler
    {
        private readonly IUserRepository userRepository;

        public ConsultHandler(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public async Task<ResponseBase<User>> Get(GetUserByIdRequest getUserByIdRequest)
        {
            var response = new ResponseBase<User>(success: false, errors: new List<string>(), data: null);
            var user = await userRepository.FindById(getUserByIdRequest);

            if (user == null)
            {
                var error = new NotFoundError();
                response.Errors.Add($"{error.Code}:{error.Message.ToString()}");
            }

            response.Success = true;
            response.Data = user;
            return response;
        }

        public async Task<ResponseBase<PagedResult<User>>> Get(GetUsersRequest getUsersRequest)
        {
            var response = new ResponseBase<PagedResult<User>>(success: false, errors: new List<string>(), data: null);

            var users = await userRepository.Find(getUsersRequest);

            if (users == null)
            {
                var error = new NotFoundError();
                response.Errors.Add($"{error.Code}:{error.Message.ToString()}");
            }

            response.Success = true;
            response.Data = users;
            return response;
        }

        public async Task<ResponseBase<PagedResult<User>>> Get(GetFollowedUsersRequest getUsersRequest)
        {
            var response = new ResponseBase<PagedResult<User>>(success: false, errors: new List<string>(), data: null);

            var users = await userRepository.Find(getUsersRequest);

            if (users == null)
            {
                var error = new NotFoundError();
                response.Errors.Add($"{error.Code}:{error.Message.ToString()}");
            }

            response.Success = true;
            response.Data = users;
            return response;
        }
    }
}
