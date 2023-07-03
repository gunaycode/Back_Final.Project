using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BlogDto
{
    public class GetBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TextAll { get; set; } = null!;
        public string FAQs { get; set; } = null!;
        public List<GetBlogImageDto> BlogImages { get; set; }
    }

    public class GetBlogImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
