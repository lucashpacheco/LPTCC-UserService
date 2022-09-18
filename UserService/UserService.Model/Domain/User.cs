using System;
using Microsoft.AspNetCore.Identity;

namespace UserService.Model.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string id , string name , string email)
        {
            this.Id = id;
            this.Name = name;  
            this.Email = email;
        }

        public User()
        {
        }
    }
}
