using Domain.Entities.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.HotelDto;

namespace Application.DTOs.RoomDto
{
    public class GetRoomDto
    {
        public int Id { get; set; }
        public int RoomCategoryId { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public int Count { get; set; }
        public List<GetImageRoomDto> Images { get; set; }

    }
    public class GetImageRoomDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
