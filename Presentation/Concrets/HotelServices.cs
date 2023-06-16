using Application.Abstract;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using Persistance.Extantion;
using System.Security.Cryptography.X509Certificates;

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
                Price = postHotelDto.Price,
               
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
                Price = (decimal)hotel.Price
            };
        }

        //public Task<GetHotelDto> UpdateAsync(PostHotelDto postHotelDto, int Id)
        //{
            




        //}
        //public async Task<GetHotelDto> UpdateAsync(int id, UpdateHotelDto updateHotelDto)
        //{
        //    Hotel hotel= await _context.Hotels.FindAsync(id);

        //    if (hotel == null)
        //    {
        //        throw new NotFoundException("Hotel not found");
        //    }

        //    hotel.Name = updateHotelDto.Name;
        //    hotel.Rating = updateHotelDto.Rating;
        //    hotel.Price = updateHotelDto.Price;

        //    if (updateHotelDto.Images != null)
        //    {
        //        foreach (IFormFile file in updateHotelDto.Images)
        //        {
        //            if (file.CheckFileSize(8000000)) // 1024 bolur
        //            {
        //                throw new FileTypeException("Check exception");
        //            }

        //            if (!file.CheckFileType("image/"))
        //            {
        //                throw new FileSizeException();
        //            }

        //            string newFileName = await file.FileUploadAsync(_webHostEnvironment.WebRootPath, "Images");

        //            hotel.Images.Add(new ImageHotel()
        //            {
        //                ImageName = newFileName,
        //            });
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return new GetHotelDto
        //    {
        //        Id = hotel.Id,
        //        Name = hotel.Name,
        //        Rating = hotel.Rating,
        //        Price = (decimal)hotel.Price
        //    };
        //}



    }
}
