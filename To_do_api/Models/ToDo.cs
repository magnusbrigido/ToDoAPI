using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_api.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ToDo(Guid user_id, string title, string description)
        {
            Id = Guid.NewGuid();
            User_Id = user_id;
            Title = title;
            Description = description;
            Done = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
