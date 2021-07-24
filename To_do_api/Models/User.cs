using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_api.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            IsAdmin = false;
            CreatedAt = DateTime.Now;
            i++;
        }
    }
}
