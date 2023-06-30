using Application.Abstract;
using Application.DTOs.SearchDto;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class SearchServices:ISearchResultServices
    {
        private List<SearchResult> _searchResults;

        public SearchServices()
        {
            _searchResults = new List<SearchResult>();
        }

        public async Task<List<SearchResult>> Search(int count, string city, DateTime date)
        {
            List<SearchResult> results = _searchResults
             .Where(result => result.Count == count && result.City == city && result.Date.Date == date.Date)
             .ToList();
            List<SearchResult> searchResultDtos = results.Select(result => new SearchResult
            {
                City = result.City,
                Date = result.Date,
                Count = result.Count
            }).ToList();

            return searchResultDtos;
        }
    }
}
