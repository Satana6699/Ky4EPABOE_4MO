using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Application.Dtos
{
    public class UserForCreationDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }
    }
}
