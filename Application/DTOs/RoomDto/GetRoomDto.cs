using Domain.Entities.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RoomDto
{
    public class GetRoomDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int UsertId { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        
       
       
    }
}
