using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDto
{
    public class GetUserDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime UserBirthDate { get; set; } //backround service 
    }
}
