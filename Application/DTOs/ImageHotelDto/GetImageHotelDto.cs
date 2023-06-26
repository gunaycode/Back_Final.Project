using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageHotelDto
{
    public class GetImageHotelDto
    {
        public int hotelId { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; } = null!;


    }
}
