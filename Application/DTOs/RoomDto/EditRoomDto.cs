using Domain.Entities.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.RoomDto
{
    public class EditRoomDto
    {
        
        public int RoomCategoryId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        
    }
}
