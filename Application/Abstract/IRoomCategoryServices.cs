using Application.DTOs.RoomCategory;
using Application.DTOs.RoomCategoryDto;
using Application.DTOs.RoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public  interface IRoomCategoryServices
    {
        Task<GetRoomCategoryDto> CreateAsync(CreateRoomCategoryDto createRoomCategory);
        Task<GetRoomCategoryDto>EditAsync(EditRoomCategoryDto editRoomCategory,int id);
        Task DeleteAsync(int id);   
        Task<GetRoomCategoryDto> GetByIdAsync(int id);
        Task<GetRoomCategoryDto> GetAll();

    }
}
