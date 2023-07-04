using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Reservation:BaseAuditable
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public RoomCategory RoomCategory { get; set; }
        public int RoomCategoryId { get; set; } 
        public int Count { get; set; }

       
    }
}
