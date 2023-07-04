using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Application.DTOs.ImageRoomDto;
using Application.DTOs.ResponseDto;
using Application.DTOs.RoomDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using Travel_project.EXception;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class RoomController:ControllerBase
    {
        readonly IRoomServices _services;
        readonly TravelDbContext _context;
        readonly IWebHostEnvironment _environment;
        public RoomController(IRoomServices services, IWebHostEnvironment environment, TravelDbContext context)
        {
            _services = services;
            _environment = environment;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateRoomDto createRoom)
        {
            try
            {
                return Ok(await _services.CreateAsync(createRoom));
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
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EditRoomDto editRoom)
        {
            try
            {
                return Ok(await _services.UpdateAsync(editRoom, id));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _services.DeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return StatusCode(200, await _context.Rooms.Include(i => i.RoomImages).ToListAsync());
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
            Room? room = await _context.Rooms.Include(i => i.RoomImages).FirstOrDefaultAsync(h => h.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, room);
        }
        [HttpPost("{roomId}/Images")]
        public async Task<ActionResult> UpdateImagesRoomAsync(int roomId, [FromForm] EditImagesRoomDto images)
        {
            try
            {
                return Ok(await _services.UpdateRoomImagesAsync(images, roomId));
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
