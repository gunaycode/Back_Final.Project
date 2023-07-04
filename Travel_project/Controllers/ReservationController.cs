using Application.Abstract;
using Application.DTOs.HotelDto;
using Application.DTOs.ReservationDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using Stripe;
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

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] List<CreateReservationDto> CreateReservationDtos)
        //{
        //    try
        //    {
        //        return Ok(await _reservationServices.CreateAsync(CreateReservationDtos));
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new ResponseDto()
        //        {
        //            Message = ex.Message,
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<List<GetReservationDto>>> CreateAsync(List<CreateReservationDto> reservationDto)
        {
            try
            {
                List<GetReservationDto> createdReservations = await _reservationServices.CreateAsync(reservationDto);
                return createdReservations;
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                
                return BadRequest(ex.Response.Content);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _reservationServices.ReservationDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("/api/Reservations")]
        public async Task<IActionResult> GettAll()
        {
            try
            {
                return StatusCode(200, await _context.Reservations.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ex.Message
                });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            Reservation? reservation = await _context.Reservations.FirstOrDefaultAsync(h => h.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, reservation);
        }
    }
}
