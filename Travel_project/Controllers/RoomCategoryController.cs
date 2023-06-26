using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.ResponseDto;
using Application.DTOs.RoomCategory;
using Application.DTOs.RoomCategoryDto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using System.Data;

namespace Travel_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomCategoryController:ControllerBase
    {
        private readonly TravelDbContext _context;
        public readonly IRoomCategoryServices _services;

        public RoomCategoryController(TravelDbContext context, IRoomCategoryServices services)
        {
            _context = context;
            _services= services;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRoomCategoryDto createRoomCategory)
        {
            try
            {
                return Ok(await _services.CreateAsync(createRoomCategory));
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
        public async Task<IActionResult> EditAsync(int id, [FromBody] EditRoomCategoryDto editRoomCategory)
        {
            try
            {
                return Ok(await _services.EditAsync(editRoomCategory, id));
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
                await _services.DeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("/api/RoomCategories")]
        public async Task<IActionResult> GettAll()
        {
            try
            {
                return StatusCode(200, await _context.RoomCategories.ToListAsync());
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
            RoomCategory? roomCategory = await _context.RoomCategories.FirstOrDefaultAsync(h => h.Id == id);
            if (roomCategory == null)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status200OK, roomCategory);
        }



    }
}
