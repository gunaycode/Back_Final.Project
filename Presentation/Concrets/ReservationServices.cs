using Application.Abstract;
using Application.DTOs.ReservationDto;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class ReservationServices : IReservationServices
    {
        private readonly TravelDbContext _dbcontext;
        
        public Task<GetReservationDto> CreateAsync(CreateReservationDto reservationDto)
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDto> UpdateAsync(EditReservationDto reservationDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
