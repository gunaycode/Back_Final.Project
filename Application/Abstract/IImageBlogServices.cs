using Application.DTOs.ImageBlogDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IImageBlogServices
    {
        Task<GetImageBlogDto> CreateAsync(CreateImageBlogDto imageBlogPost);
    }
}
