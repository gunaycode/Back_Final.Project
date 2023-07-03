using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BlogDto
{
    public class CreateBlogDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TextAll { get; set; } = null!;
        public string FAQs { get; set; } = null!;
        public List<IFormFile> Images { get; set; }
    }
}
