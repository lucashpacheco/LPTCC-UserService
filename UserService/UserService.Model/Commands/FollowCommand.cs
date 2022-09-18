using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Commands
{
    public class FollowCommand
    {
        public Guid UserId { get; set; }
        public Guid FollowedUserId { get; set; }
    }
}
