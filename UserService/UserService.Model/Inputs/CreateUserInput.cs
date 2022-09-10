using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Model.Inputs
{
    public class CreateUserInput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
