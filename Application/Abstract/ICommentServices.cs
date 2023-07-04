using Application.DTOs.CommentDto;
using System;
using Application.DTOs.CommentDto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.ReservationDto;

namespace Application.Abstract
{
    public interface ICommentServices
    {
        Task<GetCommentDto> CreateAsync(CreateCommentDto commnet);
        Task<GetCommentDto> GetByIdAsync(int id);
        Task<GetCommentDto> GetAllAsync();
        Task CommentDeleteAsync(int id);
        

    }
}
