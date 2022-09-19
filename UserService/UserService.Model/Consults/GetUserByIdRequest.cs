using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Consults
{
    public class GetUserByIdRequest
    {
        [Required(ErrorMessage = "UserId size is required")]
        public Guid? UserId { get; set; }
    }
}
