using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Errors
{
    public class UnauthorizedError : GenericError
    {
        public UnauthorizedError()
        {
            this.Code = Constants.Errors.UnauthorizedCode;
            this.Message = Constants.Errors.UnauthorizedMessage;
        }
    }
}
