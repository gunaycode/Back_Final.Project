using Application.DTOs.CommentDto;
using System;
using Application.DTOs.CommentDto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICommentServices
    {
        Task<GetCommentDto> CreateAsync(CreateCommentDto commnet);
        Task CommentDeleteAsync(int id);
        
    }
}
