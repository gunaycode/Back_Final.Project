using Application.Abstract;
using Application.DTOs.CommentDto;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using System.Linq.Expressions;
using System.Net;

namespace Travel_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",Roles = "SuperAdmin")]
    public class CommentController:ControllerBase
    {

        readonly ICommentServices _services;
        readonly TravelDbContext _context;
        public CommentController(ICommentServices services, TravelDbContext context)
        {
            _services = services;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
        {
            try
            {
                return Ok(await _services.CreateAsync(commentDto));
            }
            catch (NotFoundException ex) { return NotFound(new ResponseDto { Message = ex.Message }); }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            
            try
            {
                await _services.CommentDeleteAsync(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
            
        }

        [HttpPost("/api/Comments")]
        public async Task<IActionResult> GettAll()
        {
            try
            {
                return StatusCode(200, await _context.Comments.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ex.Message
                });
            }
        }


    }
}
