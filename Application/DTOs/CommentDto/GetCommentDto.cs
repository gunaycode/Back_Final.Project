using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CommentDto
{
    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public int HotelId { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }


    }
}
