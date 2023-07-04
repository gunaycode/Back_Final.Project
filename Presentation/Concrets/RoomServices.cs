using Application.Abstract;
using Application.Abstract.Common;
using Application.DTOs.FileService;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageRoomDto;
using Application.DTOs.RoomDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;
namespace Persistance.Concrets
{
    public class RoomServices : IRoomServices
    {
        private TravelDbContext _dbcontext;
        public IWebHostEnvironment _environment;
        private readonly IAzureFileService _fileService;

        public RoomServices(TravelDbContext dbContext, IWebHostEnvironment environment, IAzureFileService azureFile)
        {
            _dbcontext = dbContext;
            _environment = environment;
            _fileService = azureFile;
        }
        public async Task<GetRoomDto> CreateAsync(CreateRoomDto roomDto)
        {
            Room room = new Room()
            {
                Price = roomDto.Price,
                HotelId = roomDto.HotelId,
                CategoryNameId = roomDto.RoomCategoryId,
                Count = roomDto.Count

            };
            if (roomDto.Images != null)
            {
                foreach (IFormFile file in roomDto.Images)
                {
                    if (file.CheckFileSize(8000000))
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    //string newFileName = await file.FileUploadAsync(_environment.WebRootPath, "ImagesRoom");
                    FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("roomimages", file);

                    room.RoomImages.Add(new ImageRoom()
                    {
                        ImageName = fileUploadResult.fileName,
                        Path = $"https://travelapi.blob.core.windows.net/roomimages/{fileUploadResult.fileName}",
                    });
                }
                _dbcontext.Rooms.Add(room);
                await _dbcontext.SaveChangesAsync();
            }
            return new GetRoomDto
            {
                Id = room.Id,
                Price = room.Price,
                HotelId = room.HotelId,
                RoomCategoryId = room.CategoryNameId,
                Count = room.Count,
                Images = room.RoomImages.Select(i => new GetImageRoomDto()
                {
                    Id = i.Id,
                    Url = $"https://travelapi.blob.core.windows.net/roomimages/{i.ImageName}"
                }).ToList()
            };
        }
        public async Task<GetRoomDto> GetByIdAsync(int id)
        {
            Room? room = await _dbcontext.Rooms.FirstOrDefaultAsync(h => h.Id == id) ??
                 throw new NotFoundException();
            return new GetRoomDto { Id = room.Id, RoomCategoryId = room.CategoryNameId, HotelId = room.HotelId, Images = (List<GetImageRoomDto>)room.RoomImages };
        }
        public async Task<GetRoomDto> UpdateAsync(EditRoomDto roomDto, int id)
        {
            Room? room = await _dbcontext.Rooms.FindAsync(id);

            if (room == null)
            {
                throw new NotFoundException("Room not found");
            }
            room.HotelId = roomDto.HotelId;
            room.Price = roomDto.Price;
            room.CategoryNameId = roomDto.RoomCategoryId;
            room.Count = roomDto.Count;
            await _dbcontext.SaveChangesAsync();

            return new GetRoomDto
            {
                Id = room.Id,
                HotelId = room.HotelId,
                Price = room.Price,
                RoomCategoryId = room.CategoryNameId,
                Count = roomDto.Count
            };
        }
        public async Task<GetRoomDto> DeleteAsync(int id)
        {
            Room? room = await _dbcontext.Rooms.FindAsync(id)
               ?? throw new NotFoundException("Hotel not found");
            _dbcontext.Rooms.Remove(room);
            await _dbcontext.SaveChangesAsync();
            return new GetRoomDto();
        }
        public async Task<List<GetRoomDto>> GetAllAsync()
        {
            List<Room>? rooms = await _dbcontext.Rooms.ToListAsync() ??
               throw new NotFoundException();
            List<GetRoomDto> getRoomDtos = rooms.Select(h => new GetRoomDto
            {
                Id = h.Id,
                HotelId = h.HotelId,
                Price = h.Price,
                RoomCategoryId = h.CategoryNameId,
                Count = h.Count,
                Images = h.RoomImages.Select(i => new GetImageRoomDto()
                {
                    Id = i.Id,
                    Url = $"https://travelapi.blob.core.windows.net/roomimages/{i.ImageName}"
                }).ToList()

            }).ToList();
            return getRoomDtos;
        }
        public async Task<List<GetImagesRoomDto>> UpdateRoomImagesAsync(EditImagesRoomDto editImagesRoom, int roomId)
        {
            Room? room = await _dbcontext.Rooms.FindAsync(roomId);
            if (room == null)
            {
                throw new NotFoundException("Room not found");
            }
            List<GetImagesRoomDto> updatedImages = new List<GetImagesRoomDto>();

            foreach (IFormFile image in editImagesRoom.Images)
            {
                if (image.CheckFileSize(8000000))
                {
                    throw new FileTypeException("Check exception");
                }
                if (!image.CheckFileType("image/"))
                {
                    throw new FileSizeException();
                }
                string newFileName = await image.FileUploadAsync(_environment.WebRootPath, "ImagesRoom");
                FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("roomimages", image);
                ImageRoom newImage = new ImageRoom
                {
                    ImageName = newFileName,
                    RoomId = roomId,
                    Path = $"https://travelapi.blob.core.windows.net/{fileUploadResult.filePath}"
                };
                room.RoomImages.Add(newImage);
                updatedImages.Add(new GetImagesRoomDto
                {
                    ImageName = newFileName,
                    roomId = roomId,
                    Url = $"https://travelapi.blob.core.windows.net/{fileUploadResult.filePath}"

                });
            }
            await _dbcontext.SaveChangesAsync();
            return updatedImages;
        }
    }
}
