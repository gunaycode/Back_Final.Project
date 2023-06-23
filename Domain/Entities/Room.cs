using Domain.Entities.Base;

namespace Domain.Entities
{
    public class Room:BaseAuditable
    {
        public Room()
        {

            RoomImages = new HashSet<ImageRoom>();
        }
        
        public string RoomName { get; set; }
        public int UsertId { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ImageRoom> RoomImages { get; set; }
        

    }
}
