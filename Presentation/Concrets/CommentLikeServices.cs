using Application.Abstract;
using Application.DTOs.CommentDto;
using Application.DTOs.CommentLike;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class CommentLikeServices:ICommentLikeServices
    {
        private readonly TravelDbContext _context;
        public readonly ICurrentServices _current;
        public CommentLikeServices(TravelDbContext context, ICurrentServices current)
        {
            _context = context;
            _current = current;
        }
        public async Task<int> LikeComment(int commentId)
        {
            var loginId = _current.UserId;

            var existingLike = await _context.CommentLikes      
                .FirstOrDefaultAsync(l => l.CommentId == commentId && l.UserId == loginId);
            if (existingLike != null)
            {
                throw new Exception("You have already liked this comment.");
            }
            var newLike = new CommentLike
            {
                CommentId = commentId,
                UserId = (int)loginId
            };
            _context.CommentLikes.Add(newLike);
            await _context.SaveChangesAsync();

            int totalLikes = await _context.CommentLikes.CountAsync(l => l.CommentId == commentId);
            return totalLikes;
        }


        public async Task CommentLikeDelete( int commentId)
        {
            CommentLike? commentLike = await _context.CommentLikes.FindAsync(commentId) ?? throw new NotfoundException();
            _context.CommentLikes.Remove(commentLike);
            await _context.SaveChangesAsync();
        }

        
    }
}
