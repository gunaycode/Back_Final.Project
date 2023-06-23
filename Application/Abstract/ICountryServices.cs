using Application.DTOs.CountryDto;
using Application.DTOs.HotelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICountryServices
    {
        Task<GetCountryDto> CreateAsync(PostCountryDto postCountryDto);
        Task<GetCountryDto> UpdateAsync(PostCountryDto postCountryDto, int id);
        Task CountryDeleteAsync(int id);
        Task<GetCountryDto> GetAllAsync();
        Task<GetCountryDto> GetByIdAsync(int id);

    }
}
