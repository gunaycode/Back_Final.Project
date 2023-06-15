using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PostImageHotelDto
    {
        public string Text { get; set; } = null!;
        public User User { get; set; } = null!;
        public string UserId { get; set; }
    }
}
