using Application.Abstract;
using Application.DTOs.HotelDto;
using Application.DTOs.ResponseDto;
using Application.DTOs.RoomDto;
using Microsoft.AspNetCore.Mvc;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

    }
}
