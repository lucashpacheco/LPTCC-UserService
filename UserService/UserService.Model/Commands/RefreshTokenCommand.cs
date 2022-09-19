using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model.Commands
{
    public class RefreshTokenCommand
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
