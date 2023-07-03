using Application.Abstract;
using Application.DTOs.BlogDto;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageBlogDto;
using Application.DTOs.ImageHotelDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController:ControllerBase
    {
        readonly IBlogServices _services;
        readonly TravelDbContext _context;
        readonly IWebHostEnvironment _environment;
        public BlogController(IBlogServices services, IWebHostEnvironment environment, TravelDbContext context)
        {
            _services = services;
            _environment = environment;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto createBlogDto)
        {
            try
            {
                return Ok(await _services.CreateAsync(createBlogDto));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ResponseDto()
                {
                    Message = ex.Message,
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return StatusCode(200, await _context.Blogs.Include(i => i.Images).ToListAsync());
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    MessagePack = ex.Message,
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            Blog? blog = await _context.Blogs.Include(i => i.Images).FirstOrDefaultAsync(h => h.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, blog);
        }
        [HttpGet("Images/{ImageName}")]

        public async Task<IActionResult> GetImagesAsync([FromRoute] string ImageName)
        {
            var file = await _context.ImagesHotel.FirstOrDefaultAsync(f => f.ImageName == ImageName)
                ?? throw new Exception("Image not found");

            string path = Path.Combine(_environment.WebRootPath, "Images", file.ImageName);
            if (!System.IO.File.Exists(path))
                throw new Exception("File not found");

            FileExtensionContentTypeProvider provider = new();
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);


            if (provider.TryGetContentType(path, out string? contentType))
                contentType = "application/octet-stream";

            return File(imageBytes, contentType);
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] EditBlogDto editBlogDto)
        {
            try
            {
                return Ok(await _services.EditAsync(editBlogDto, id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("{blogId}/Images")]
        public async Task<ActionResult> UpdateImagesHotelAsync(int blogId, [FromForm] EditImageBlogDto images)
        {
            try
            {
                return Ok(await _services.UpdateImagesHotelAsync(images,blogId ));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FileTypeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FileSizeException)
            {
                return BadRequest("Invalid file size");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("{blogId}")]
        public async Task<IActionResult> DeleteAsync(int blogId)
        {
            try
            {
                await _services.DeleteAsync(blogId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
