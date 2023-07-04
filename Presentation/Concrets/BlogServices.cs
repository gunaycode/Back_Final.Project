using Application.Abstract;
using Application.Abstract.Common;
using Application.DTOs.BlogDto;
using Application.DTOs.FileService;
using Application.DTOs.ImageBlogDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;

namespace Persistance.Concrets
{
    public class BlogServices : IBlogServices
    {
        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAzureFileService _fileService;

        public BlogServices(TravelDbContext context, IWebHostEnvironment webHostEnvironment, IAzureFileService azureFileService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = azureFileService;
        }

        public async Task<GetBlogDto> CreateAsync(CreateBlogDto createBlogDto)
        {
            Blog blog = new Blog
            {
              Title = createBlogDto.Title,
              Description = createBlogDto.Description,
              TextAll = createBlogDto.TextAll,
              FAQs = createBlogDto.FAQs,
            };
            if (blog.Images != null)
            {
                foreach (IFormFile file in createBlogDto.Images)
                {
                    if (file.CheckFileSize(8000000))//1024 bolur
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    //string newFileName = await file.FileUploadAsync(_webHostEnvironment.WebRootPath, "ImagesBlog");
                    FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("blogimages", file);
                    blog.Images.Add(new ImageBlog()
                    {
                        ImageName = fileUploadResult.fileName,
                        Path = $"https://travelapi.blog.core.windows.net/blogimages/{fileUploadResult.fileName}",
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
                FAQs= blog.FAQs,
                BlogImages = blog.Images.Select(i => new GetBlogImageDto()
                {
                    Id = i.Id,
                    Url = $"https://travelapi.blog.core.windows.net/blogimages/{i.ImageName}"
                }).ToList()
            };
        }

        public async Task<GetBlogDto> DeleteAsync(int blogId)
        {
            Blog? blog = await _context.Blogs.FindAsync(blogId);
            if (blog == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return new GetBlogDto();
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
            blog.FAQs = editBlogDto.FAQs;
           
            await _context.SaveChangesAsync();

            return new GetBlogDto
            {
                Id = blog.Id,
                TextAll = blog.TextAll,
                Description = editBlogDto.Description,
                Title = editBlogDto.Title,
                FAQs= editBlogDto.FAQs,
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
                    Url = $"https://travelapi.blog.core.windows.net/blogimages/{i.ImageName}"
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
                FAQs = blog.FAQs,
                TextAll=blog.TextAll,BlogImages = (List<GetBlogImageDto>)blog.Images };
        }

        public async Task<List<GetImageBlogDto>> UpdateImagesHotelAsync(EditImageBlogDto editImageBlogDto, int blogId)
        {
            Blog? blog = await _context.Blogs.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == blogId);
            if (blog == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            List<GetImageBlogDto> updatedImages = new List<GetImageBlogDto>();
            List<ImageBlog> updatedImagesBlog = new List<ImageBlog>();


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
                FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("blogimages", image);
                ImageBlog newImage = new ImageBlog
                {
                    ImageName = newFileName,
                    BlogId = blogId, 
                    Path = $"https://travelapi.blog.core.windows.net/blogimages/{fileUploadResult.filePath}"
                };
                updatedImagesBlog.Add(newImage);
                foreach (var item in blog.Images)
                {
                    blog.Images.Remove(item);
                }

                blog.Images.Add(newImage);
                updatedImages.Add(new GetImageBlogDto
                {
                    ImageName = newImage.ImageName,
                    blogId=blogId,
                    Url = $"https://travelapi.blog.core.windows.net/blogimages/{fileUploadResult.filePath}"
                });
            }
            blog.Images = updatedImagesBlog;
            await _context.SaveChangesAsync();
            return updatedImages;
        }
    }
}
