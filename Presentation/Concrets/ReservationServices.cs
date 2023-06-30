using Application.Abstract;
using Application.DTOs.ReservationDto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        public ReservationServices(TravelDbContext context)
        {
            _dbcontext = context; 
        }
        public async Task<List<GetReservationDto>> CreateAsync(List<CreateReservationDto> reservationDto)
        {
            List<Reservation> reservationsCreate = reservationDto.Select(reservationDto => new Reservation
            {
                UserId = reservationDto.UserId,
                RoomCategoryId=reservationDto.RoomCategoryId,
                Date = reservationDto.Date,
                Count=reservationDto.Count,
                RoomId=reservationDto.RoomId,
            }).ToList();

            _dbcontext.Reservations.AddRange(reservationsCreate);
            await _dbcontext.SaveChangesAsync();

            List<GetReservationDto> createdReservations = reservationsCreate.Select(reservation => new GetReservationDto
            {
                Id = reservation.Id,
                UserId = reservation.UserId,
                RoomCategoryId = reservation.RoomCategoryId,
                Count=reservation.Count,
                Date = reservation.Date,
                RoomId=reservation.RoomId,
            }).ToList();

            return createdReservations;
        }

        public Task<GetReservationDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
