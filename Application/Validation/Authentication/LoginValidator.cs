using Application.DTOs.AuthDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Authentication
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator() 
        {
            RuleFor(u => u.Username).NotEmpty().NotNull();
            RuleFor(u => u.Password).NotEmpty().NotNull();
        }
    }
}
