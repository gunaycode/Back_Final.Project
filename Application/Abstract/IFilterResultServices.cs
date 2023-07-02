using Application.DTOs.Filter;
using Application.DTOs.SearchDto;
using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IFilterResultServices
    { 
        Task<List<Hotel>> Filter(int? rating, decimal? MinPrice,decimal? MaxPrice ,bool? wifi, HotelLocation? location, bool? pet, bool? pool, bool? parking, bool? breakfast);
    }
}
