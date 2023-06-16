using Application.Abstract;
using Application.DTOs;
using Application.DTOs.HotelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        readonly IHotelServices _services;
        readonly IWebHostEnvironment _environment;
        public HotelController(IHotelServices services, IWebHostEnvironment environment)
        {
            _services = services;
            _environment = environment;
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





    }
}
