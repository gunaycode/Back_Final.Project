using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ICurrentServices
    {
        public int? UserId { get;  }
        public string? Username { get; }
        public string? Email { get; }

    }
}
