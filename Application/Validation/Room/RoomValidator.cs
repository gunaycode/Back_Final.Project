using Application.DTOs.CommentDto;
using Application.DTOs.RoomDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Room
{
    public class RoomValidator: AbstractValidator<CreateRoomDto>
    {
       public RoomValidator() 
       {
            RuleFor(p => p.Price)
              .NotEmpty()
              .NotNull();
       }

    }
}
