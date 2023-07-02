using Application.Abstract;
using Application.DTOs.SearchDto;
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
    public class SearchServices : ISearchResultServices
    {
        private readonly TravelDbContext _travelDb;
        public SearchServices(TravelDbContext travelDb)
        {
            _travelDb = travelDb;
        }
        public async Task<List<Hotel>> Search(int? count, int? city, DateTime? date)
        {
            var query = _travelDb.Hotels
               .Include(h => h.City)
               .Include(h => h.Rooms)
               .ThenInclude(h => h.Reservations);
            if (city != null)
            {
                query.Where(h => h.CityId == city);
            }
            if (count != null)
            {
                query.Where(h => h.Rooms.Any(r => r.Count == count));
            }
            if (date != null)
            {
                query.Where(h => h.Rooms.Any(r => r.Reservations.Any(re => re.Date != date)));
            }
            List<Hotel> hotels = await query.ToListAsync();
            return hotels;

        }

    }

}
