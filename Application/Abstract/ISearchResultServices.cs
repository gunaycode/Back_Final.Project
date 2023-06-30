using Application.DTOs.SearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ISearchResultServices
    {
        Task<List<SearchResult>> Search(int count, string city, DateTime date);
    }
}
