using Application.Abstract;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageRoomDto;
using Application.DTOs.RoomDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class RoomServices : IRoomServices
    {
        private TravelDbContext _dbcontext;
        public IWebHostEnvironment _environment;
       
        public RoomServices(TravelDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbcontext = dbContext;
            _environment = environment;
        }
        public async Task<GetRoomDto> CreateAsync(CreateRoomDto roomDto)
        {
            Room room = new Room()
            {
                RoomName = roomDto.RoomName,
                Price = roomDto.Price,
                HotelId = roomDto.HotelId,
              
            };
            if (roomDto.Images != null)
            {
                foreach (IFormFile file in roomDto.Images)
                {
                    if (file.CheckFileSize(8000000))//1024 bolur
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    string newFileName = await file.FileUploadAsync(_environment.WebRootPath, "ImagesRoom");
                    room.RoomImages.Add(new ImageRoom()
                    {
                        ImageName = newFileName,
                        Path = newFileName,
                    });
                }
                _dbcontext.Rooms.Add(room);
                await _dbcontext.SaveChangesAsync();
            }
            return new GetRoomDto
            {
                Id = room.Id,
                RoomName = room.RoomName,
                Price = room.Price,
                HotelId = room.HotelId,
                
            };
        }

        public Task<GetRoomDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetRoomDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetRoomDto> UpdateAsync(EditRoomDto roomDto, int id)
        {
            throw new NotImplementedException();
        }

    }
}
