using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model
{
    public static class Constants
    {
        public static class Errors
        {
            public const int UnauthorizedCode = 401;
            public const string UnauthorizedMessage = "Unauthorized";
            public const int BadRequestCode = 400;
            public const string BadRequestMessage = "BadRequest";
            public const int ForbbidenCode = 403;
            public const string ForbbidenMessage = "Forbbiden";
            public const int NotfoundCode = 404;
            public const string NotfoundMessage = "Notfound";
        }
    }
}
