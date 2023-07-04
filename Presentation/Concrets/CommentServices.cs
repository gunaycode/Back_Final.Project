using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.CommentDto;
using Application.DTOs.ReservationDto;
using Application.Validation.Commnet;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class CommentServices: ICommentServices
    {
        private readonly TravelDbContext _context;
        public readonly ICurrentServices _currentServices;
        public CommentServices(TravelDbContext conontext, ICurrentServices currentServices)
        {
            _context = conontext;
            _currentServices = currentServices;
        }
        public async Task<GetCommentDto> CreateAsync(CreateCommentDto comment)
        {
            var loginId = _currentServices.UserId;
            User? User= await _context.Users.FirstOrDefaultAsync(u=> u.Id==loginId)
            ?? throw new NotfoundException("User Not Found");
            Comment? newComment = new Comment
            {
                Text = comment.Text,
                UserId = (int)loginId,
                HotelId = comment.HotelId,
            };
            _context.Comments.Add(newComment);
            await  _context.SaveChangesAsync();
            return new GetCommentDto { Text = newComment.Text,Id=newComment.Id, HotelId=newComment.HotelId, UserId=newComment.UserId, Rating=newComment.Rating};
        }
        public async Task CommentDeleteAsync(int id)
        {
            Comment? comment = await _context.Comments.FindAsync(id) ?? throw new NotfoundException();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
        public async Task<GetCommentDto> GetByIdAsync(int id)
        {
           Comment ? comment = await _context.Comments.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetCommentDto
            {
               Id=comment.Id,
               UserId=comment.UserId,
               HotelId=comment.HotelId,
               Rating = comment.Rating
            };
        }

        public async Task<GetCommentDto> GetAllAsync()
        {
            List<Comment>? comments = await _context.Comments.ToListAsync() ??
                  throw new NotFoundException();
            List<GetCommentDto> getCity = comments.Select(h => new GetCommentDto
            {
                Id = h.Id,
                UserId = h.UserId,
                HotelId = h.HotelId,
                Rating=h.Rating,

                
            }).ToList();
            return new GetCommentDto { };
        }
    }
}
