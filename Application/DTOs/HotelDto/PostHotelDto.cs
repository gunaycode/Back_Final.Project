using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.HotelDto
{
    public class PostHotelDto
    {
        
        public string Name { get; set; } = null!;
        public int Rating { get; set; }
        public int CityId { get; set; }
        public bool WiFi { get; set; }
        public bool Pool { get; set; }
        public bool Parking { get; set; }
        public string Location { get; set; }
        public bool Breakfast { get; set; }
        public bool Pet { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
