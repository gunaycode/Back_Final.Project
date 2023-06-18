using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.HotelDto
{
    public class UpdateHotelDto
    {
       
        public string Name { get; set; } = null!;
        public int Rating { get; set; }
        public decimal Price { get; set; }

        
    }
}
