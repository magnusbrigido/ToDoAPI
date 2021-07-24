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
    [Route("api/User/ToDo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDo> ToDos = new List<ToDo>();

        [HttpGet]
        [Route("{user_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUsersToDo(Guid user_id)
        {
            List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

            if (!ToDosByUser.Any()) return NotFound("To-dos not found");

            return Ok(ToDosByUser);
        }

        [HttpPost]
        [Route("{user_id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateToDo(Guid user_id, [FromBody] CreateToDoDTO ToDoDTO)
        {
            if (string.IsNullOrEmpty(ToDoDTO.Title)
                || string.IsNullOrEmpty(ToDoDTO.Description)
                )  return BadRequest("Title and Description must be filled");
            
            ToDo toDo = new ToDo(user_id, ToDoDTO.Title, ToDoDTO.Description);
            
            ToDos.Add(toDo);
            
            return CreatedAtAction("Post", new { id = toDo.Id },toDo);
        }

        [HttpPatch]
        [Route("{user_id}/{todo_id}/done")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult TaskComplete(Guid user_id, Guid todo_id)
        {
            List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

            if (!ToDosByUser.Any()) return NotFound("User does not have to-dos");
                
            ToDo ToDoFound = ToDosByUser.FirstOrDefault(todo => todo.Id == todo_id);
                
            if (ToDoFound == null) return NotFound("To-do not found");
                
            ToDoFound.Done = true;
                
            ToDoFound.UpdatedAt = DateTime.Now;
                
            return Ok();
        }

        [HttpPut]
        [Route("{user_id}/{todo_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Edit(Guid user_id, Guid todo_id, [FromBody] CreateToDoDTO toDoDTO)
        {
            List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

            if (!ToDosByUser.Any()) return NotFound("User does not have to-dos");

            ToDo ToDoFound = ToDosByUser.FirstOrDefault(todo => todo.Id == todo_id);

            if (ToDoFound == null) return NotFound("To-do not found");

            ToDoFound.Title = toDoDTO.Title;
            
            ToDoFound.Description = toDoDTO.Description;

            ToDoFound.UpdatedAt = DateTime.Now;

            return Ok();        
        }
    }
}
