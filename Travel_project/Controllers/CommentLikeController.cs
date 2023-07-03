using Application.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public readonly ICommentServices _commentService;
        public CommentLikeController(ICommentServices commentServices, TravelDbContext context, ICommentLikeServices commentLikeServices)
        {
            _commentLike = commentLikeServices;
            _context = context;
            _commentService = commentServices;
        }
         
        [HttpPost("comment/{commentId}")]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
                
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value; 
                                                
                int totalLikes = await _commentLike.LikeComment(int.Parse(userId),commentId);
                return Ok(new { TotalLikes = totalLikes });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
