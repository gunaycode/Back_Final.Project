using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CommentLike
{
    public class GetCommnetLikeDto
    {
        public int Id { get; set; }
        public DateTime LikeDate { get; set; }
        public string UserId { get; set; }

    }
}
