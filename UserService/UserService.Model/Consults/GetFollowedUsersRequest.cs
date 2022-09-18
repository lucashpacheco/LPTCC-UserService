using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Model.Responses.Common;

namespace UserService.Model.Consults
{
    public class GetFollowedUsersRequest
    {
        public Guid UserId { get; set; }

        public PageInformation PageInformation { get; set; }
    }
}
