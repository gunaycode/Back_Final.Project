using Application.Abstract;
using Application.DTOs.CommentLike;
using Application.DTOs.ResponseDto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.Concrets;
using Persistance.DataContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Travel_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommentLikeController:ControllerBase
    {
        private readonly ICommentLikeServices _commentLike;
        private readonly TravelDbContext _context;
       
        public CommentLikeController( TravelDbContext context, ICommentLikeServices commentLikeServices)
        {
            _commentLike = commentLikeServices;
            _context = context;
          
        }
         
        [HttpPost]
        public async Task<IActionResult> LikeComment([FromBody] CreateLikeCommentDto commentId)
        {
            try
            {
                  int totalLikes = await _commentLike.LikeComment( commentId.CommentId);
                   return Ok(new { TotalLikes = totalLikes }); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _commentLike.CommentLikeDelete(id);
                return StatusCode(StatusCodes.Status204NoContent, new ResponseDto { Status = "Successs", Message = "Comment delete successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new ResponseDto { Status = "Error", Message = ex.Message });
            }
        }
    }
}
