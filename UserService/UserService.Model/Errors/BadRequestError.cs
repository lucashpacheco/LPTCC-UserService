using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Errors
{
    public class BadRequestError : GenericError
    {
        public BadRequestError()
        {
            this.Code = Constants.Errors.BadRequestCode;
            this.Message = Constants.Errors.BadRequestMessage;
        }
    }
}
