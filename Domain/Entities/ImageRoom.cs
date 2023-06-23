using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ImageRoom:BaseAuditable
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string ImageName { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
