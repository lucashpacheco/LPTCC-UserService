using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Commands
{
    public class FollowCommand
    {
        [Required]
        public Guid? UserId { get; set; }
        
        [Required]
        public Guid? FollowedUserId { get; set; }
    }
}
