using Application.DTOs;
using Application.DTOs.CityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICityServices
    {
        Task<GetCityDto> AsyncCreate(PostCityDto postCityDto);
    }
}
