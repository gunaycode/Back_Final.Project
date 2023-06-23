using Application.DTOs.ImageHotelDto;
using Application.DTOs.ImageRoomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IImagesRoomServices
    {
        Task<GetImagesRoomDto> CreateAsync(CreateImagesRoomDto createImagesRoom);
    }
}
