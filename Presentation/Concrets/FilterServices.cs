using Application.Abstract;
using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using Domain.Entities;
using Domain.Entities.Enum;
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
        public async Task<List<Hotel>> Filter(int? rating, decimal? MinPrice,decimal? MaxPrice ,bool? wifi, HotelLocation? location, bool? pet, bool? pool, bool? parking, bool? breakfast)
        {
            var query = _context.Hotels
            .Include(r => r.Rooms);
            if(rating == null)
            {
                query.Where(r=>r.Rating==rating);
            }
            if (MinPrice != null && MaxPrice != null)
            {
                query.Where(x => x.Rooms.Any(r =>  r.Price > MinPrice && r.Price < MaxPrice ));
            }
            if (pet != null)
            {
                query.Where(p=>p.Pet==pet);
            }
            if (pool != null)
            {
                query.Where(p=>p.Pool==pool);
            }
            if(location!=null)
            {
                query.Where(l => l.Location == location);
            }
            if(wifi!= null)
            {
                query.Where(w=>w.WiFi==wifi);
            }
            if(parking!= null)
            {
                query.Where(p=>p.Parking==parking);
            }
            if(breakfast!= null)
            {
                query.Where(b=>b.Breakfast==breakfast);   
            }
            List<Hotel> hotels = await query.ToListAsync();
            return hotels;

        }

        //        var query = _travelDb.Hotels
        //               .Include(h => h.City)
        //               .Include(h => h.Rooms)
        //               .ThenInclude(h => h.Reservations);
        //            if (city != null)
        //            {
        //                query.Where(h => h.CityId == city);
        //            }
        //            if (count != null)
        //            {
        //                query.Where(h => h.Rooms.Any(r => r.Count == count));
        //            }
        //              if (date != null)
        //            {
        //              query.Where(h => h.Rooms.Any(r => r.Reservations.Any(re => re.Date != date)));
        //            }
        //List<Hotel> hotels = await query.ToListAsync();
        //            return hotels;
        //    }
    }
}

