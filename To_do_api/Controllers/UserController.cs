using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_do_api.Models;
using To_do_api.DTOs;

namespace To_do_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> Users = new List<User>();

        [HttpGet]
        public List<User> Get()
        {
            return Users;
        }

        [HttpGet]
        [Route("{Id}")]
        public User GetUserById(int Id)
        {
            User UserToGet = Users.FirstOrDefault(user => user.Id == Id);

            if (UserToGet != null) return UserToGet;

            throw new Exception("User not found");
        }

        [HttpPost]
        public void Post([FromBody] CreateUserDTO UserDTO)
        {
            if (!string.IsNullOrEmpty(UserDTO.Email))
            {
                User UserFound = Users.FirstOrDefault(user => user.Email == UserDTO.Email);

                if(UserFound != null)
                {
                    Users.Add(new User(UserDTO.Name, UserDTO.Email, UserDTO.Password));

                    return;
                }

                throw new Exception("User already exists");
            }

            throw new Exception("Invalid E-mail");
        }

        [HttpPatch]
        [Route("newPassword/{id}")]
        public void PatchPassword([FromForm] string NewPassword, int Id)
        {
            User UserToUpdate = Users.FirstOrDefault(user => user.Id == Id);

            if (UserToUpdate != null)
            {
                UserToUpdate.Password = NewPassword;

                return;
            }

            throw new Exception("User not found");
        }

        [HttpPatch]
        [Route("{id}")]
        public void PatchAdmin(int Id)
        {
            User UserToBeAdmin = Users.FirstOrDefault(user => user.Id == Id);

            if (UserToBeAdmin != null)
            {
                UserToBeAdmin.IsAdmin = true;

                return;
            }

            throw new Exception("User not found");
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int Id)
        {
            User UserToDelete = Users.FirstOrDefault(user => user.Id == Id);
            
            if (UserToDelete != null)
            {
                Users.Remove(UserToDelete);

                return;
            }

            throw new Exception("User not found");
        }
    }
}
