using Application.DTOs.CityDto;
using Application.DTOs.CountryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICityServices
    {
        Task<GetCityDto> CreateAsync(PostCityDto postCityDto);
        Task<GetCityDto> UpdateAsync(PostCityDto postCityDto, int id );
        Task CityDeleteAsync(int id);
        Task<GetCityDto> GetAllAsync();
        Task<GetCityDto> GetByIdAsync(int id);

    }
}
