using Application.DTOs.AuthDto;
using Application.DTOs.CommentDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Commnet
{
    public class CommnetCreateVlidator: AbstractValidator<CreateCommentDto>
    {
      public CommnetCreateVlidator() 
      {
            RuleFor(p => p.Text)
              .NotEmpty()
              .NotNull();
      }
    }
}
