using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_api.DTOs
{
    public class CreateToDoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
