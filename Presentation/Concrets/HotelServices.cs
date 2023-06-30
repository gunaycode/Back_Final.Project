using Application.Abstract;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Persistance.Concrets
{
    public class HotelServices : IHotelServices
    {

        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelServices(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<GetHotelDto> CreateAsync(PostHotelDto postHotelDto)
        {
            Hotel hotel = new Hotel
            {
                Name = postHotelDto.Name,
                Rating = postHotelDto.Rating,
                CityId=postHotelDto.CityId,
                Parking=postHotelDto.Parking,
                Pet=postHotelDto.Pet,
                Pool=postHotelDto.Pool,
                Breakfast=postHotelDto.Breakfast,
                Location=postHotelDto.Location,
                WiFi=postHotelDto.WiFi, 
            };
            if (hotel.Images != null)
            {
                foreach (IFormFile file in postHotelDto.Images)
                {
                    if (file.CheckFileSize(8000000))//1024 bolur
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    string newFileName = await file.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");
                    hotel.Images.Add(new ImageHotel()
                    {
                        ImageName = newFileName,
                        Path = newFileName,
                    });
                }
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
            }
            return new GetHotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Rating = hotel.Rating,
                CityId = hotel.CityId,
                Parking=hotel.Parking,
                Pet=hotel.Pet,
                Pool=hotel.Pool,
                Location=hotel.Location,
                Breakfast=hotel.Breakfast,
                WiFi=hotel.WiFi,
                Images = hotel.Images.Select(i => new GetImageDto()
                {
                    Id = i.Id,
                    Url = $"https://localhost:7046/api/Hotel/Images/{i.ImageName}"
                }).ToList()
            };
        }
        public async Task<GetHotelDto> UpdateAsync(UpdateHotelDto updateHotelDto, int id)
        {
            Hotel? hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            hotel.Name = updateHotelDto.Name;
            hotel.Rating = updateHotelDto.Rating;
            hotel.CityId= updateHotelDto.CityId;
        
            await _context.SaveChangesAsync();

            return new GetHotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Rating = hotel.Rating,
                CityId = hotel.CityId,
                Parking= hotel.Parking,
                Pet= hotel.Pet,
                Breakfast= hotel.Breakfast,
                Location= hotel.Location,
                WiFi=hotel.WiFi,
                Pool=hotel.Pool,

            };
        }

        public async Task<GetHotelDto> GetByIdAsync(int id)
        {
            Hotel? hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id) ??
                throw new NotFoundException();
            return new GetHotelDto { Id = hotel.Id, Name = hotel.Name, Rating = hotel.Rating,Images= (List<GetImageDto>)hotel.Images };
        }
        public async Task<List<GetHotelDto>> GetAllAsync()
        {
            List<Hotel>? hotels= await _context.Hotels.ToListAsync()??
                throw new NotFoundException();
            List<GetHotelDto> getHotelDtos = hotels.Select(h => new GetHotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Rating = h.Rating,
                CityId = h.CityId,
                WiFi=h.WiFi,
                Parking=h.Parking,
                Pet=h.Pet,
                Pool=h.Pool,
                Breakfast=h.Breakfast,
                Location=h.Location,
                Images = h.Images.Select(i => new GetImageDto()
                {
                    Id = i.Id,
                    Url = $"https://localhost:7046/api/Hotel/Images/{i.ImageName}"
                }).ToList()
                

            }).ToList();
            return getHotelDtos;
        }
        public async Task <List<GetImageHotelDto>> UpdateImagesHotelAsync(UpdateImagesHotelDto updateImageHotelDto, int hotelId)
        {
            Hotel? hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            List<GetImageHotelDto> updatedImages = new List<GetImageHotelDto>();

            foreach (IFormFile image in updateImageHotelDto.Images)
            {
                if (image.CheckFileSize(8000000)) 
                {
                    throw new FileTypeException("Check exception");
                }
                if (!image.CheckFileType("image/"))
                {
                    throw new FileSizeException();
                }
                string newFileName = await image.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");
                ImageHotel newImage = new ImageHotel
                {
                    ImageName = newFileName,
                    HotelId = hotelId,
                    Path=Path.Combine(_webHostEnvironment.WebRootPath, "Images")    
                };
                hotel.Images.Add(newImage);
                updatedImages.Add(new GetImageHotelDto
                {
                   
                    ImageName = newImage.ImageName,
                    hotelId = hotelId,
                    Url=$"https://localhost:7046/api/Hotel/Images/{newImage.ImageName}"
                });
            }
            await _context.SaveChangesAsync();
            return updatedImages;

        }

        
    }

}
