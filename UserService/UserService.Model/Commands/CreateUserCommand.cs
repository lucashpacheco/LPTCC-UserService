using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserService.Model.Commands
{
    public class CreateUserCommand
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 8 and 50 characters", MinimumLength = 5)]
        public string Password { get; set; }

    }
}
