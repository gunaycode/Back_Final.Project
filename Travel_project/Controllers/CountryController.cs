using Application.Abstract;
using Application.DTOs.CountryDto;
using Application.DTOs.HotelDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController:ControllerBase
    {
        private readonly TravelDbContext _context;
        public readonly ICountryServices _servives;
        public CountryController(TravelDbContext context, ICountryServices services)
        {
            _context = context;
            _servives = services;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostCountryDto postCountryDto)
        {
            try
            {
                return Ok(await _servives.CreateAsync(postCountryDto));
                
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostCountryDto postCountryDto)
        {
            try
            {
                return Ok(await _servives.UpdateAsync(postCountryDto, id));
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _servives.CountryDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("/api/Countries")]
        public async Task<IActionResult> GettAll()
        {
            try
            {
                return StatusCode(200, await _context.Countries.ToListAsync());
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
            Country? country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK,country);
        }
    }
}
