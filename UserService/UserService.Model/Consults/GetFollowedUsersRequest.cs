using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Model.Responses.Common;

namespace UserService.Model.Consults
{
    public class GetFollowedUsersRequest

    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "Page and page size is required")]
        public PageInformation PageInformation { get; set; }
    }
}
