using Application.Abstract;
using Application.DTOs.CountryDto;
using Application.DTOs.HotelDto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class CountryServices:ICountryServices
    {
        private TravelDbContext _context;
        
        public CountryServices(TravelDbContext context)
        {
            _context = context;
        }
        public async Task<GetCountryDto> CreateAsync(PostCountryDto postCountryDto)
        {
            Country country = new Country
            {
                Name = postCountryDto.Name,
            };
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return new GetCountryDto { Name = country.Name, };
        }

        public async Task<GetCountryDto> GetAllAsync()
        {
            List<Country>? countries = await _context.Countries.ToListAsync() ??
                throw new NotFoundException();
            List<GetCountryDto> getCountry = countries.Select(h => new GetCountryDto
            {
                Id = h.Id,
                Name = h.Name,
            }).ToList();
            return new GetCountryDto { };
        }

        public async Task<GetCountryDto> UpdateAsync(PostCountryDto updateCountryDto, int id)
        {
            Country? country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                throw new NotFoundException("Country not found");
            }

            country.Name = updateCountryDto.Name;
            
            await _context.SaveChangesAsync();

            return new GetCountryDto
            {
                Id = country.Id,
                Name = country.Name,
            };
        }
        public async Task CountryDeleteAsync(int id)
        {
            Country? country = await _context.Countries.FindAsync(id) ?? throw new NotfoundException();
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task<GetCountryDto> GetByIdAsync(int id)
        {
            Country? country = await _context.Countries.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetCountryDto { Id = country.Id, Name = country.Name};
        }

        
    }
}
