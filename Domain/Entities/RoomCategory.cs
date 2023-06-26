using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomCategory:BaseAuditable
    {
        public string CategoryName { get; set; } = null!;
        
        public ICollection<Room> Rooms { get; set; }
    }
}
