using Application.DTOs.ImageHotelDto;
using Application.DTOs.ImageRoomDto;
using Application.DTOs.ReservationDto;
using Application.DTOs.RoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IRoomServices
    {
        Task<GetRoomDto> CreateAsync(CreateRoomDto roomDto);
        Task<GetRoomDto> UpdateAsync(EditRoomDto roomDto, int id);
        Task<List<GetImagesRoomDto>> UpdateRoomImagesAsync(EditImagesRoomDto editImagesRoom, int roomId);
        Task<GetRoomDto> GetByIdAsync(int id);
        Task<GetRoomDto>DeleteAsync(int id);
        Task<List<GetRoomDto>> GetAllAsync();

    }
}
