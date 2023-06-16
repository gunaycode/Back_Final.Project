using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Rating { get; set; }
        
        public decimal Price { get; set; } 

    }
}
