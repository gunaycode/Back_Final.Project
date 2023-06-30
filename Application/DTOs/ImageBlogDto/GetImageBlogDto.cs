using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ImageBlogDto
{
    public class GetImageBlogDto
    {
        public int blogId { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; } = null!;

    }
}
