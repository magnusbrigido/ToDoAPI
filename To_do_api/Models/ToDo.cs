using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_api.Models
{
    public class ToDo
    {
        private static int i = 1;
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public ToDo(int user_id, string title, string description)
        {
            Id = i;
            User_Id = user_id;
            Title = title;
            Description = description;
            Done = false;
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
            i++;
        }
    }
}
