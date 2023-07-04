using Application.Abstract;
using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.DataContext;
using Persistance.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistance.Concrets
{
    public class FilterServices : IFilterResultServices
    {
        private readonly TravelDbContext _context;
        public FilterServices(TravelDbContext context)
        {
            _context = context;
        }
        public async Task<List<Hotel>> Filter([FromQuery] int? rating, [FromQuery] decimal? MinPrice, [FromQuery] decimal? MaxPrice, [FromQuery] bool? wifi, [FromQuery] HotelLocation? location, [FromQuery] bool? pet, [FromQuery] bool? pool, [FromQuery] bool? parking, [FromQuery] bool? breakfast)
        {
            var query = _context.Hotels
            .AsQueryable();
            if (rating == null)
            {
              query=  query.Include(c=>c.Comments).Where(c => c.Comments.Any(c => c.Rating == rating)).OrderBy(c => c.Comments.OrderBy(c => c.Rating));
            }
            if (MinPrice != null && MaxPrice != null)
            {
              query=  query.Include(c=>c.Rooms).Where(x => x.Rooms.Any(r => r.Price > MinPrice && r.Price < MaxPrice));
            }
            if (pet != null)
            {
               query= query.Where(p => p.Pet == pet);
            }
            if (pool != null)
            {
               query= query.Where(p => p.Pool == pool);
            }
            if (location != null)
            {
               query= query.Where(l => l.Location == location);
            }
            if (wifi != null)
            {
                query=query.Where(w => w.WiFi == wifi);
            }
            if (parking != null)
            {
               query= query.Where(p => p.Parking == parking);
            }
            if (breakfast != null)
            {
               query= query.Where(b => b.Breakfast == breakfast);
            }
            List<Hotel> hotels = await query.ToListAsync();
            return hotels;

        }

    }
}

