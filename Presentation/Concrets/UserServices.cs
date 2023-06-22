using Application.Abstract;
using Application.DTOs.UserDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class UserServices:IUserServices
    {
        public readonly TravelDbContext _context;
        public UserServices(TravelDbContext context) {  _context = context; }

        
        public async Task<UpdateUserDto> UpdateUserAsync([FromBody]UpdateUserDto user, [FromRoute] int id)
        {
            User? newUser = await _context.Users.FirstOrDefaultAsync(s => s.Id == id)
           ?? throw new NotfoundException();
            newUser.UserName = user.UserName;
            
            await _context.SaveChangesAsync();
            return new()
            {
                UserName = user.UserName,
                
            };
        }
    }
}
