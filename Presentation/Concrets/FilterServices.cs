using Application.Abstract;
using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class FilterServices:IFilterResultServices
    {
        private List<FilterResult> _filterResults;
        public FilterServices()
        {
            _filterResults = new List<FilterResult>();
        }
        public async Task<List<FilterResult>> Filter(int rating, int price, bool wifi, string location, bool pet, bool pool, bool parking, bool breakfast)
        {
            List<FilterResult> results = _filterResults
            .Where(result => result.Price == price
            && result.Breakfast == breakfast 
            && result.Location==location 
            && result.Pool==pool
            && result.WiFi== wifi
            && result.Pet==pet
            && result.Parking==parking
            &&result.Rating==rating
           
            ).ToList();
            List<FilterResult> filterResult = results.Select(result => new FilterResult
            {
                Parking = result.Parking,
                Pet = result.Pet,
                Location = result.Location,
                Pool=result.Pool,
                WiFi=result.WiFi,
                Rating=result.Rating,
                Breakfast=result.Breakfast,
                Price=result.Price,
                
            }).ToList();

            return filterResult;

        }
    }
}
