using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CommentDto
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int HotelId { get ; set; }   
        
       

    }
}
