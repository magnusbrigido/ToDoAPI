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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetUsers()
        {
            if(Users.Any()) return Ok(Users);

            return NotFound("There is no users");
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID");

            User UserToGet = Users.FirstOrDefault(user => user.Id == id);

            if (UserToGet == null) return NotFound("User not found");

            return Ok(UserToGet);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] CreateUserDTO UserDTO)
        {
            if (string.IsNullOrEmpty(UserDTO.Email)) return BadRequest("Invalid E-mail");
            
            User UserFound = Users.FirstOrDefault(user => user.Email == UserDTO.Email);
            
            if(UserFound != null) return BadRequest("User already exists");
            
            User user = new User(UserDTO.Name, UserDTO.Email, UserDTO.Password);
            
            Users.Add(user);
            
            return CreatedAtAction("Post", new { id = user.Id }, user);
        }

        [HttpPatch]
        [Route("{id}/newPassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ChangePassword(int id, [FromBody] string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword)) return BadRequest("Invalid password");

            if (id <= 0) return BadRequest("Invalid ID");

            User UserToUpdate = Users.FirstOrDefault(user => user.Id == id);

            if (UserToUpdate == null) return NotFound("User not found");

            UserToUpdate.Password = newPassword;
            
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult BecomeAdmin(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID");

            User UserToBeAdmin = Users.FirstOrDefault(user => user.Id == id);

            if (UserToBeAdmin == null) return NotFound("User not found");
            
            UserToBeAdmin.IsAdmin = true;
            
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID");

            User UserToDelete = Users.FirstOrDefault(user => user.Id == id);
            
            if (UserToDelete == null) return NotFound("User not found");
            
            Users.Remove(UserToDelete);
            
            return Ok();
        }
    }
}
