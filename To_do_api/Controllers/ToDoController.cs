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
        public IActionResult Get(int user_id)
        {
            if (user_id > 0)
            {
                List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

                if (ToDosByUser.Any()) return Ok(ToDosByUser);

                return NotFound("To-dos not found");     
            }

            return BadRequest("Invalid ID");
        }

        [HttpPost]
        [Route("{user_id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Post(int user_id, [FromBody] CreateToDoDTO ToDoDTO)
        {
            if (user_id > 0)
            {
                if (string.IsNullOrEmpty(ToDoDTO.Title)
                    || string.IsNullOrEmpty(ToDoDTO.Description)
                    )  return BadRequest("Title and Description must be filled");

                ToDo toDo = new ToDo(user_id, ToDoDTO.Title, ToDoDTO.Description);

                ToDos.Add(toDo);

                return CreatedAtAction("Post", new { id = toDo.Id },toDo);
            }

            return BadRequest("Invalid ID");
        }

        [HttpPatch]
        [Route("{user_id}/{todo_id}/done")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Patch(int user_id, int todo_id)
        {
            if(user_id > 0 && todo_id > 0)
            {
                List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

                if (ToDosByUser.Any())
                {
                    ToDo ToDoFound = ToDosByUser.FirstOrDefault(todo => todo.Id == todo_id);

                    if (ToDoFound != null)
                    {
                        ToDoFound.Done = true;

                        ToDoFound.Updated_At = DateTime.Now;

                        return Ok();
                    }

                    return NotFound("To-do not found");
                }

                return NotFound("User does not have to-dos");
            }

            return BadRequest("Invalid User or To-do ID");
        }

        [HttpPut]
        [Route("{user_id}/{todo_id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int user_id, int todo_id, [FromBody] CreateToDoDTO toDoDTO)
        {
            if (user_id > 0 && todo_id > 0)
            {
                List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

                if (ToDosByUser.Any())
                {
                    ToDo ToDoFound = ToDosByUser.FirstOrDefault(todo => todo.Id == todo_id);

                    if (ToDoFound != null)
                    {
                        if (ToDoFound.Title != toDoDTO.Title)
                            ToDoFound.Title = toDoDTO.Title;

                        if (ToDoFound.Description != toDoDTO.Description)
                            ToDoFound.Description = toDoDTO.Description;

                        ToDoFound.Updated_At = DateTime.Now;

                        return Ok();
                    }

                    return NotFound("To-do not found");
                }

                return NotFound("User does not have to-dos");
            }

            return BadRequest("Invalid User or To-do ID");
        }
    }
}
