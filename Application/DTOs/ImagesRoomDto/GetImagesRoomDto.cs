using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageRoomDto
{
    public class GetImagesRoomDto
    {
        public int roomId { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; } = null!;

    }
}
