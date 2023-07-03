using Application.DTOs.BlogDto;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageBlogDto;
using Application.DTOs.ImageHotelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IBlogServices
    {
        Task<GetBlogDto> CreateAsync(CreateBlogDto createBlogDto);
        Task<GetBlogDto> EditAsync(EditBlogDto editBlogDto, int id);
        Task<List<GetImageBlogDto>> UpdateImagesHotelAsync(EditImageBlogDto editImageBlogDto, int blogId);
        Task<GetBlogDto> GetByIdAsync(int id);
        Task<List<GetBlogDto>> GetAllAsync();
        Task<GetBlogDto> DeleteAsync(int hotelId);

    }
}
