using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PostHotelDto
    {
        public string Name { get; set; } = null!;
        public int Rating { get; set; }
        public decimal Price { get; set; }
       
        public List<IFormFile>Images { get; set; }
        
    }
}
