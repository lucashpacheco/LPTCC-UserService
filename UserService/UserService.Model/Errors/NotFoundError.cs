using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Errors
{
    public class NotFoundError : GenericError
    {
        public NotFoundError()
        {
            this.Code = Constants.Errors.ForbbidenCode;
            this.Message = Constants.Errors.ForbbidenMessage;
        }
    }
}
