using Application.DTOs;
using Application.DTOs.CommentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICommentLikeServices
    {
        Task<GetCommentLikeDto> AsyncCreate(PostImageHotelDto postCommentDto);
    }
}
