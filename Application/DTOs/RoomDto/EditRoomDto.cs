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
        public string RoomName { get; set; }
        public int CityId { get; set; }
        public int RoomCategoryId { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        
    }
}
