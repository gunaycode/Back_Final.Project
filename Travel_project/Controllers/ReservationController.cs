using Application.Abstract;
using Microsoft.AspNetCore.Mvc;
using Persistance.DataContext;

namespace Travel_project.Controllers
{
    public class ReservationController:ControllerBase
    {
        private readonly TravelDbContext _context;
        public readonly IReservationServices _reservationServices;
        public ReservationController(TravelDbContext context, IReservationServices reservationServices)
        {
            _context = context;
            _reservationServices = reservationServices;
        }
        
    }
}
