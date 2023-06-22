using Application.DTOs.AuthDto;
using Application.DTOs.HotelDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Hotel
{
    public class HotelValidator: AbstractValidator<PostHotelDto>
    {
        public HotelValidator() 
        {
            RuleFor(p => p.Name).MinimumLength(5).MaximumLength(30).NotNull().NotEmpty();
            

        }
    }
}
