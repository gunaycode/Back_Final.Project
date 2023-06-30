using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageBlogDto
{
    public class CreateImageBlogDto
    {
        public IFormFileCollection Images { get; set; }

    }
}
