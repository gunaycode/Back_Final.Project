using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ResponseDto
{
    public class ResponseDto
    {
        public string Message { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
