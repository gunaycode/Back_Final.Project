using Application.DTOs.CommentDto;
using Application.DTOs.CommentLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICommentLikeServices
    {
        Task<int> LikeComment(int commentId);
        Task CommentLikeDelete(int commentId);
       
        
    }
}
