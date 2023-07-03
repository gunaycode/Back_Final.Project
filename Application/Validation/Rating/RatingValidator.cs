using Application.DTOs.HotelDto;
using Application.DTOs.RatingDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Rating
{
    public class RatingValidator: AbstractValidator<RatingCreate>
    {
        public RatingValidator()
        {
            RuleFor(r => r.MinBy)
              .GreaterThanOrEqualTo(0)
              .WithMessage("Minimum değer 0 olmalıdır.");

            RuleFor(r => r.MaxBy)
                .InclusiveBetween(0, 5)
                .WithMessage("Maksimum değer 5 aralığında olmalıdır.");
        }

    }
}
