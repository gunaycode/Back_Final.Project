using Domain.Entities.Base;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Room:BaseAuditable
    {
        public Room()
        {

            RoomImages = new HashSet<ImageRoom>();
        }
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int UsertId { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ImageRoom> RoomImages { get; set; }
        

    }
}
