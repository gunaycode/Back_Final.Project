using Application.Abstract;
using Application.DTOs.BlogDto;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageBlogDto;
using Application.DTOs.ImageHotelDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class BlogServices : IBlogServices
    {
        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogServices(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<GetBlogDto> CreateAsync(CreateBlogDto createBlogDto)
        {
            Blog blog = new Blog
            {
              Title = createBlogDto.Title,
              Description = createBlogDto.Description,
              TextAll = createBlogDto.TextAll,
              Date=createBlogDto.Date,
            };
            if (blog.Images != null)
            {
                foreach (IFormFile file in createBlogDto.Images)
                {
                    if (file.CheckFileSize(8000000))//1024 bolur
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    string newFileName = await file.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");
                    blog.Images.Add(new ImageBlog()
                    {
                        ImageName = newFileName,
                        Path = newFileName,
                    });
                }
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
            }
            return new GetBlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Description= blog.Description,
                TextAll= blog.TextAll,
                BlogImages = blog.Images.Select(i => new GetBlogImageDto()
                {
                    Id = i.Id,
                    Url = $"https://localhost:7046/api/Blog/Images/{i.ImageName}"
                }).ToList()
            };
        }
        public async Task<GetBlogDto> EditAsync(EditBlogDto editBlogDto, int id)
        {
            Blog? blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                throw new NotFoundException("Blog not found");
            }
            blog.Title = editBlogDto.Title;
            blog.Description = editBlogDto.Description;
            blog.TextAll = editBlogDto.TextAll;
            blog.Date=editBlogDto.Date;
            await _context.SaveChangesAsync();

            return new GetBlogDto
            {
                Id = blog.Id,
                TextAll = blog.TextAll,
                Date = blog.Date,
                Description = editBlogDto.Description,
                Title = editBlogDto.Title,
            };
        }
        public async Task<List<GetBlogDto>> GetAllAsync()
        {
            List<Blog>? blogs = await _context.Blogs.ToListAsync() ??
                throw new NotFoundException();
            List<GetBlogDto> getBlogDtos = blogs.Select(h => new GetBlogDto
            {
                Id = h.Id,
               
                BlogImages = h.Images.Select(i => new GetBlogImageDto()
                {
                    Id = i.Id,
                    Url = $"https://localhost:7046/api/Hotel/Images/{i.ImageName}"
                }).ToList()


            }).ToList();
            return getBlogDtos;
        }

        public async Task<GetBlogDto> GetByIdAsync(int id)
        {
            Blog? blog = await _context.Blogs.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetBlogDto { Id = blog.Id, Title=blog.Title,
                Description=blog.Description,
                TextAll=blog.TextAll,Date=blog.Date,BlogImages = (List<GetBlogImageDto>)blog.Images };
        }

        public async Task<List<GetImageBlogDto>> UpdateImagesHotelAsync(EditImageBlogDto editImageBlogDto, int blogId)
        {
            Blog? blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            List<GetImageBlogDto> updatedImages = new List<GetImageBlogDto>();

            foreach (IFormFile image in editImageBlogDto.Images)
            {
                if (image.CheckFileSize(8000000))
                {
                    throw new FileTypeException("Check exception");
                }
                if (!image.CheckFileType("image/"))
                {
                    throw new FileSizeException();
                }
                string newFileName = await image.FileUploadAsync(_webHostEnvironment.WebRootPath, "ImagesBlog");
                ImageBlog newImage = new ImageBlog
                {
                    ImageName = newFileName,
                    BlogId = blogId,
                    
                    Path = Path.Combine(_webHostEnvironment.WebRootPath, "BlogImages")
                };
                blog.Images.Add(newImage);
                updatedImages.Add(new GetImageBlogDto
                {
                    ImageName = newImage.ImageName,
                    blogId=blogId,
                    Url = $"https://localhost:7046/api/Blog/Images/{newImage.ImageName}"
                });
            }
            await _context.SaveChangesAsync();
            return updatedImages;
        }
    }
}
