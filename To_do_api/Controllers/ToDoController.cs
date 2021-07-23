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
    [Route("api/User")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDo> ToDos = new List<ToDo>();

        [HttpGet]
        [Route("{user_id}/ToDos")]
        public List<ToDo> Get(int user_id)
        {
            if (user_id > 0)
            {
                List<ToDo> ToDosByUser = ToDos.FindAll(todo => todo.User_Id == user_id);

                if (ToDosByUser.Any()) return ToDosByUser;

                throw new Exception("To-dos not found");     
            }

            throw new Exception("Invalid ID");
        }

        [HttpPost]
        [Route("{user_id}")]
        public void Post([FromBody] CreateToDoDTO ToDoDTO, int user_id)
        {
            if (user_id > 0)
            {
                if (string.IsNullOrEmpty(ToDoDTO.Title)
                    || string.IsNullOrEmpty(ToDoDTO.Description)
                    )  throw new Exception("Title and Description must be filled");

                ToDos.Add(
                    new ToDo(user_id, ToDoDTO.Title, ToDoDTO.Description)
                    );

                return;
            }

            throw new Exception("Invalid ID");
        }

        [HttpPatch]
        [Route("{user_id}/{todo_id}/done")]
        public void Patch(int user_id, int todo_id)
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

                        return;
                    }

                    throw new Exception("To-do not found");
                }

                throw new Exception("User does not have to-dos");
            }

            throw new Exception("Invalid User or To-do ID");
        }

        [HttpPut]
        [Route("{user_id}/{todo_id}")]
        public void Put(int user_id, int todo_id, [FromBody] CreateToDoDTO toDoDTO)
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

                        return;
                    }

                    throw new Exception("To-do not found");
                }

                throw new Exception("User does not have to-dos");
            }

            throw new Exception("Invalid User or To-do ID");
        }
    }
}
