using Application.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUserServices
    {
        Task<UpdateUserDto> UpdateUserAsync(UpdateUserDto updateUserDto, int id);
    }
}
