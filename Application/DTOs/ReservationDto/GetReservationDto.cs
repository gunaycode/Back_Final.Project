using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ReservationDto
{
    public class GetReservationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
        public int RoomCategoryId { get; set; }
        public int RoomId { get; set;}

    }
}
