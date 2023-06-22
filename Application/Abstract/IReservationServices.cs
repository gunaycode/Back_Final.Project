using Application.DTOs.HotelDto;
using Application.DTOs.ImageHotelDto;
using Application.DTOs.ReservationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IReservationServices
    {
        Task <GetReservationDto> CreateAsync(CreateReservationDto reservationDto);
        Task<GetReservationDto> UpdateAsync( EditReservationDto reservationDto ,int id);
        Task<GetReservationDto> GetByIdAsync(int id);
        Task<GetReservationDto> GetAllAsync();

    }
}
