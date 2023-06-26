using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.RoomCategory;
using Application.DTOs.RoomCategoryDto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class RoomCategoryServices:IRoomCategoryServices
    {
        private readonly TravelDbContext _context;

        public RoomCategoryServices(TravelDbContext context)
        {
            _context = context;
        }
        public async Task<GetRoomCategoryDto> CreateAsync(CreateRoomCategoryDto createRoomCategory)
        {
            RoomCategory city = new RoomCategory()
            {
                CategoryName = createRoomCategory.CategoryName,
                
            };
            _context.RoomCategories.Add(city);
            await _context.SaveChangesAsync();
            return new GetRoomCategoryDto {CategoryName=city.CategoryName, Id=city.Id};
        }
        public async Task DeleteAsync(int id)
        {
            RoomCategory? roomCategory = await _context.RoomCategories.FindAsync(id) ?? throw new NotfoundException();
            _context.RoomCategories.Remove(roomCategory);
            await _context.SaveChangesAsync(); 
        }

        public async Task<GetRoomCategoryDto> EditAsync(EditRoomCategoryDto editRoomCategory, int id)
        {
            RoomCategory? roomCategory = await _context.RoomCategories.FindAsync(id);

            if (roomCategory == null)
            {
                throw new NotFoundException("Category not found");
            }

            roomCategory.CategoryName = editRoomCategory.CategoryName;

            await _context.SaveChangesAsync();

            return new GetRoomCategoryDto
            {
                Id = roomCategory.Id,
                CategoryName =roomCategory.CategoryName, 
            };
        }
        public async Task<GetRoomCategoryDto> GetAll()
        {
            List<RoomCategory>? roomCategories = await _context.RoomCategories.ToListAsync() ??
               throw new NotFoundException();
            List<GetRoomCategoryDto> getRoomCategory = roomCategories.Select(h => new GetRoomCategoryDto
            {
                Id = h.Id,
                CategoryName=h.CategoryName,
                
            }).ToList();
            return new GetRoomCategoryDto { };
        }

        public async Task<GetRoomCategoryDto> GetByIdAsync(int id)
        {
            RoomCategory? roomCategory = await _context.RoomCategories.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetRoomCategoryDto { Id = roomCategory.Id, CategoryName=roomCategory.CategoryName };
        }
    }
}
