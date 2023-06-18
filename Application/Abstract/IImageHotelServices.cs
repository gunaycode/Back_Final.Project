using Application.DTOs.CommentDto;
using Application.DTOs.ImageHotelDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IImageHotelServices
    {
        Task<GetImageHotelDto> AsyncCreate(PostImageHotelDto postImageHotelDto);
       

    }
}
