using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.CountryDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController:ControllerBase
    {
        private readonly TravelDbContext _context;
        public readonly ICityServices _cityServices;
        public CityController(TravelDbContext context, ICityServices cityServices )
        {
            _cityServices = cityServices;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Create([FromForm] PostCityDto postCityDto)
        {
            try
            {
                return Ok(await _cityServices.CreateAsync(postCityDto));

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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PostCityDto postCityDto)
        {
            try
            {
                return Ok(await _cityServices.UpdateAsync(postCityDto, id));
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
                await _cityServices.CityDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("/api/Cities")]
        public async Task<IActionResult> GettAll()
        {
            try
            {
                return StatusCode(200, await _context.Cities.ToListAsync());
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
            City? city = await _context.Cities.FirstOrDefaultAsync(h => h.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, city);
        }

    } 
}
