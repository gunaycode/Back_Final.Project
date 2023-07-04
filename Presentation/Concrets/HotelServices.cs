using Application.Abstract;
using Application.Abstract.Common;
using Application.DTOs.FileService;
using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;

namespace Persistance.Concrets
{
    public class HotelServices : IHotelServices
    {

        private readonly TravelDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAzureFileService _fileService;
        

        public HotelServices(TravelDbContext context, IWebHostEnvironment webHostEnvironment, IAzureFileService fileService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        public async Task<GetHotelDto> CreateAsync(PostHotelDto postHotelDto)
        {
            Hotel hotel = new Hotel
            {
                Name = postHotelDto.Name,
                CityId = postHotelDto.CityId,
                Parking = postHotelDto.Parking,
                Pet = postHotelDto.Pet,
                Pool = postHotelDto.Pool,
                Location = postHotelDto.Location,
                Breakfast = postHotelDto.Breakfast,
                WiFi = postHotelDto.WiFi,
            };
            if (hotel.Images != null)
            {
                foreach (IFormFile file in postHotelDto.Images)
                {
                    if (file.CheckFileSize(8000000))
                        throw new FileTypeException("Check exception");
                    if (!file.CheckFileType("image/"))
                        throw new FileSizeException();
                    //string newFileName = await file.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");
                    FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("hotelimages", file);

                    hotel.Images.Add(new ImageHotel()
                    {
                        ImageName = fileUploadResult.fileName,
                        Path = $"https://travelapi.blob.core.windows.net/hotelimages/{fileUploadResult.fileName}"
                    });
                }
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
            }
            return new GetHotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                CityId = hotel.CityId,
                Parking = hotel.Parking,
                Pet = hotel.Pet,
                Pool = hotel.Pool,
                Location = postHotelDto.Location,
                Breakfast = hotel.Breakfast,
                WiFi = hotel.WiFi,
                Images = hotel.Images.Select(i => new GetImageDto()
                {
                    Id = i.Id,
                    Url = $"https://travelapi.blob.core.windows.net/hotelimages/{i.ImageName}"
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
            hotel.CityId = updateHotelDto.CityId;
            hotel.Breakfast = updateHotelDto.Breakfast;
            hotel.Parking = updateHotelDto.Parking;
            hotel.Location = updateHotelDto.Location;
            hotel.Pet = updateHotelDto.Pet;
            hotel.Pool = updateHotelDto.Pool;
            hotel.WiFi = updateHotelDto.WiFi;
            await _context.SaveChangesAsync();

            return new GetHotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                CityId = hotel.CityId,
                Parking = hotel.Parking,
                Pet = hotel.Pet,
                Breakfast = hotel.Breakfast,
                Location = hotel.Location,
                WiFi = hotel.WiFi,
                Pool = hotel.Pool,

            };
        }
        public async Task<GetHotelDto> GetByIdAsync(int id)
        {
            Hotel? hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id) ??
                throw new NotFoundException();
            return new GetHotelDto
            {
                Id = hotel.Id, 
                Name = hotel.Name,
                Images = (List<GetImageDto>)hotel.Images,
                CityId=hotel.CityId,
                Parking=hotel.Parking,
                Pet = hotel.Pet,
                Breakfast = hotel.Breakfast,
                Location = hotel.Location,
                WiFi = hotel.WiFi,
                Pool = hotel.Pool,
            };
        }
        public async Task<List<GetHotelDto>> GetAllAsync()
        {
            List<Hotel>? hotels = await _context.Hotels.ToListAsync() ??
                throw new NotFoundException();
            List<GetHotelDto> getHotelDtos = hotels.Select(h => new GetHotelDto
            {
                Id = h.Id,
                Name = h.Name,
                CityId = h.CityId,
                WiFi = h.WiFi,
                Parking = h.Parking,
                Pet = h.Pet,
                Pool = h.Pool,
                Breakfast = h.Breakfast,
                Location = h.Location,

                Images = h.Images.Select(i => new GetImageDto()
                {
                    Id = i.Id,
                    Url = $"https://travelapi.blob.core.windows.net/hotelimages/{i.ImageName}"
                }).ToList()


            }).ToList();
            return getHotelDtos;
        }
        public async Task<List<GetImageHotelDto>> UpdateImagesHotelAsync(UpdateImagesHotelDto updateImageHotelDto, int hotelId)
        {
            Hotel? hotel = await _context.Hotels.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == hotelId);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }
            List<GetImageHotelDto> updatedImages = new List<GetImageHotelDto>();
            List<ImageHotel> updatedImagesHotel = new List<ImageHotel>();

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
                //string newFileName = await image.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");
                FileUploadResult fileUploadResult = await _fileService.UploudFileAsync("hotelimages", image);
                ImageHotel newImage = new ImageHotel
                {
                    ImageName = fileUploadResult.fileName,
                    HotelId = hotelId,
                    Path = $"https://travelapi.blob.core.windows.net/{fileUploadResult.filePath}"
                };
                updatedImagesHotel.Add(newImage);
                updatedImages.Add(new GetImageHotelDto
                {

                    ImageName = fileUploadResult.fileName,
                    hotelId = hotelId,
                    Url = $"https://travelapi.blob.core.windows.net/{fileUploadResult.filePath}"
                });
            }
            hotel.Images = updatedImagesHotel;
            await _context.SaveChangesAsync();
            return updatedImages;
        }

        public async Task<GetHotelDto> DeleteAsync(int hotelId)
        {
            Hotel? hotel = await _context.Hotels.FindAsync(hotelId)
                ?? throw new NotFoundException("Hotel not found");
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return new GetHotelDto();
        }
    }

}
