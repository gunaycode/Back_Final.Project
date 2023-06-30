using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFilterResultServices
    {
       
        Task<List<FilterResult>> Filter(int rating, int price, bool wifi, string location, bool pet, bool pool, bool parking, bool breakfast);
    }
}
