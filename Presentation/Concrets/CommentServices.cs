using Application.Abstract;
using Application.DTOs.CommentDto;
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
            };
            _context.Comments.Add(newComment);
            await  _context.SaveChangesAsync();
            return new GetCommentDto { Text = newComment.Text,Id=newComment.Id};
        }
        public async Task CommentDeleteAsync(int id)
        {
            Comment? comment = await _context.Comments.FindAsync(id) ?? throw new NotfoundException();
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
        

    }
}
