using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.CountryDto;
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
    public class CityServices : ICityServices
    {
        private TravelDbContext _context;
        public CityServices(TravelDbContext context)
        {
            _context = context;
        }
        public async Task<GetCityDto> CreateAsync(PostCityDto postCityDto)
        {
            City city = new City()
            {
               Name = postCityDto.Name,
               CountryId = postCityDto.CountryId,
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return new GetCityDto { Name = city.Name,CountryId=city.CountryId , Id=city.Id};
        }
        public async Task CityDeleteAsync(int id)
        {
            City? city = await _context.Cities.FindAsync(id) ?? throw new NotfoundException();
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<GetCityDto> UpdateAsync(PostCityDto postCityDto, int id)
        {
            City? city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                throw new NotFoundException("City not found");
            }

            city.Name = postCityDto.Name;
            city.CountryId = postCityDto.CountryId;

            await _context.SaveChangesAsync();

            return new GetCityDto
            {
                Id = city.Id,
                Name = city.Name,
                CountryId=city.CountryId,
            };
        }

        public async Task<GetCityDto> GetAllAsync()
        {
            List<City>? cities = await _context.Cities.ToListAsync() ??
                throw new NotFoundException();
            List<GetCityDto> getCity = cities.Select(h => new GetCityDto
            {
                Id = h.Id,
                Name = h.Name,
                CountryId = h.CountryId,
            }).ToList();
            return new GetCityDto { };
        }

        public async Task<GetCityDto> GetByIdAsync(int id)
        {
            City? city = await _context.Cities.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetCityDto { Id = city.Id, Name = city.Name,CountryId=city.CountryId };
        }
    }
}
