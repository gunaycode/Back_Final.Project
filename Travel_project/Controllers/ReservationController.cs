using Application.Abstract;
using Application.DTOs.HotelDto;
using Application.DTOs.ReservationDto;
using Application.DTOs.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Persistance.DataContext;
using Travel_project.EXception;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController:ControllerBase
    {
        private readonly TravelDbContext _context;
        public readonly IReservationServices _reservationServices;
        public ReservationController(TravelDbContext context, IReservationServices reservationServices)
        {
            _context = context;
            _reservationServices = reservationServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]List<CreateReservationDto> CreateReservationDtos)
        {
            try
            {
                return Ok(await _reservationServices.CreateAsync(CreateReservationDtos));
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
