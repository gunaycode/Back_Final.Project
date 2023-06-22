using Application.Abstract;
using Application.DTOs.RoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class RoomServices : IRoomServices
    {
        public Task<GetRoomDto> CreateAsync(CreateRoomDto roomDto)
        {
            throw new NotImplementedException();
        }

        public Task<GetRoomDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetRoomDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetRoomDto> UpdateAsync(EditRoomDto roomDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
