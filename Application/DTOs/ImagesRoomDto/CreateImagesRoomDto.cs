using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageRoomDto
{
    public class CreateImagesRoomDto
    {
        public IFormFileCollection Images { get; set; }
    }
}
