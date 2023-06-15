using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public  class GetImageHotelDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
