using Application.DTOs.HotelDto;
using Application.DTOs.ReservationDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Reservation
{
    public class ReservationValidator: AbstractValidator<CreateReservationDto>
    {
        public ReservationValidator() 
        {
            RuleFor(p => p.User)
              .NotEmpty()
              .NotNull();
        }
    }
}
