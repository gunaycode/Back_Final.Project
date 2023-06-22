using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageHotelDto
{
    public class ImageHotelPost
    {
        public IFormFileCollection Images { get; set; }

    }
}
