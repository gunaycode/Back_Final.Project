using Domain.Entities.Base;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotel:BaseAuditable
    {

        public Hotel()
        {
            Images = new HashSet<ImageHotel>();
        }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
        public City City { get; set; }
        public bool WiFi { get; set; }
        public bool Pool { get; set; }
        public bool Parking { get; set; }
        public HotelLocation Location { get; set; }
        public bool Breakfast { get; set; }
        public bool Pet { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<ImageHotel> Images { get; set; }
       
    }
}
