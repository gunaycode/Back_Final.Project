using Domain.Entities.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace Application.DTOs.RoomDto
{
    public class CreateRoomDto
    {
        public string RoomName { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public string City { get; set; }
        public List<IFormFile> Images { get; set; }
  
    }
}
