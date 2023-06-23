using Application.Abstract;
using Application.DTOs;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using static System.Net.WebRequestMethods;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        readonly IHotelServices _services;
        readonly TravelDbContext _context;
        readonly IWebHostEnvironment _environment;
        public HotelController(IHotelServices services, IWebHostEnvironment environment, TravelDbContext context)
        {
            _services = services;
            _environment = environment;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostHotelDto postHotelDto)
        {
            try
            {
                return Ok(await _services.CreateAsync(postHotelDto));
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
                return StatusCode(200, await _context.Hotels.ToListAsync());
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
            Hotel? hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, hotel);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDto updateHotelDto)
        {
            try
            {
                return Ok(await _services.UpdateAsync(updateHotelDto, id));
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

        [HttpPost("{hotelId}/Images")]
        public async Task<ActionResult> UpdateImagesHotelAsync(int hotelId, [FromForm] UpdateImagesHotelDto images)
        {
            try
            {
                return Ok(await _services.UpdateImagesHotelAsync(images, hotelId));
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


    }

}






    



    
    



