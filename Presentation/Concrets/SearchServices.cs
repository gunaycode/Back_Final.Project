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
        public async Task<List<Hotel>> Search(int? count, int? cityId, DateTime? date)
        {
            var query = _travelDb.Hotels
            .AsQueryable();
                    
            if (count != null)
            {
               query = query.Include(h=>h.Rooms).Where(h => h.Rooms.Any(r => r.Count == count));
            }
            if (date != null)
            {
               query= query.Include(c=>c.Rooms).ThenInclude(r=>r.Reservations).Where(h => h.Rooms.Any(r => r.Reservations.Any(re => re.Date != date)));
            }
            if (cityId != null)
            {
               query= query.Where(h => h.CityId == cityId);
            }
            List<Hotel> hotels = await query.ToListAsync();
              
            return hotels;
            
        }

    }

}
