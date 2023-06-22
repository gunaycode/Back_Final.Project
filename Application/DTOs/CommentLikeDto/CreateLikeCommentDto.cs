using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CommentLike
{
    public class CreateLikeCommentDto
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        
    }
}
