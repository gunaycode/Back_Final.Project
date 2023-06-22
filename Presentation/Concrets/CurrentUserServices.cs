using Application.Abstract;
using Microsoft.AspNetCore.Http;
using Persistance.Extantion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Concrets
{
    public class CurrentUserServices:ICurrentServices
    {
        private readonly IHttpContextAccessor _claims;

        public CurrentUserServices(IHttpContextAccessor claims)
        {
            _claims = claims;
        }
        public int? UserId => _claims.HttpContext?.User.GetLoginUserId();
        
        public string? Username => _claims.HttpContext?.User.GetLoginUserName();

        public string? Email => _claims.HttpContext?.User.GetLoginUserEmail();

        
    }
}
