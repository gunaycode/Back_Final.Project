using Application.Abstract;
using Application.DTOs.CityDto;
using Application.DTOs.ReservationDto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DataContext;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Persistance.Concrets
{
    public class ReservationServices : IReservationServices
    {
        private readonly TravelDbContext _dbcontext;
        public ReservationServices(TravelDbContext context)
        {
            _dbcontext = context; 
        }
  
        public async Task<List<GetReservationDto>> CreateAsync(List<CreateReservationDto> reservationDtoList)
        {

            var check = false;

            for (int i = 0; i < _dbcontext.Reservations.ToList().Count(); i++)
            {
                for (int j = 0; j < reservationDtoList.Count; j++)
                {
                    if (_dbcontext.Reservations.ToList()[i].RoomId == reservationDtoList[j].RoomId && _dbcontext.Reservations.ToList()[i].Date == reservationDtoList[j].Date)
                    {
                        check = true; break;
                    }
                }
            }

            var reservations = reservationDtoList.Select(reservation => new Reservation
            {
                RoomId = reservation.RoomId,
                Date = reservation.Date,
                UserId = reservation.UserId,
                RoomCategoryId = reservation.RoomCategoryId,
                Count = reservation.Count

            });

            if (check)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("404 Error: RoomId or Date cannot be null."),
                    ReasonPhrase = "Invalid Data"
                };

                throw new HttpResponseException(response);
            }
            else
            {
                _dbcontext.Reservations.AddRange(reservations);
                await _dbcontext.SaveChangesAsync();
            }


            List<Reservation> resList = new List<Reservation>();
            reservationDtoList.ForEach(reservationDto =>
            {
                resList = _dbcontext.Reservations.Where(x => x.RoomId == reservationDto.RoomId && x.Date == reservationDto.Date).ToList();
            });


            List<GetReservationDto> createdReservations = resList
                .Select(reservation => new GetReservationDto
                {
                    Id = reservation.Id,
                    UserId = reservation.UserId,
                    RoomCategoryId = reservation.RoomCategoryId,
                    Count = reservation.Count,
                    Date = reservation.Date,
                    RoomId = reservation.RoomId,
                }).ToList();

            return createdReservations;
        }


        public async Task<GetReservationDto> GetAllAsync()
        {
            List<Reservation>? reservations = await _dbcontext.Reservations.ToListAsync() ??
                 throw new NotFoundException();
            List<GetReservationDto> getreservation = reservations.Select(h => new GetReservationDto
            {
                Id = h.Id,
               UserId=h.UserId,
               RoomCategoryId = h.RoomCategoryId,
               Count=h.Count,
               Date = h.Date,
               RoomId = h.RoomId,
                
            }).ToList();
            return new GetReservationDto { };
        }
        public async Task<GetReservationDto> GetByIdAsync(int id)
        {
            Reservation? reservation = await _dbcontext.Reservations.FirstOrDefaultAsync(h => h.Id == id) ??
               throw new NotFoundException();
            return new GetReservationDto
            { 
                Id= reservation.Id,
                UserId=reservation.UserId,
                RoomCategoryId=reservation.RoomCategoryId,
                Count=reservation.Count,
                Date = reservation.Date,
                RoomId = reservation.RoomId,
            };
        }
        public async Task ReservationDeleteAsync(int id)
        {
            Reservation? reservation = await _dbcontext.Reservations.FindAsync(id) ?? throw new NotfoundException();
            _dbcontext.Reservations.Remove(reservation);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
