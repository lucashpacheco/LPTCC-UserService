using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Model.Commands
{
    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
