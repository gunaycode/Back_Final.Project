using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IHotelServices
    {
        Task<GetHotelDto> CreateAsync(PostHotelDto postHotelDto);
        Task<GetHotelDto> UpdateAsync(UpdateHotelDto updateHotelDto, int id);
        Task<List<GetImageHotelDto>> UpdateImagesHotelAsync(UpdateImagesHotelDto updateImageHotelDto, int hotelId);
        Task<GetHotelDto> GetByIdAsync(int id); 
        Task<GetHotelDto> GetAllAsync();


        
    }
}
