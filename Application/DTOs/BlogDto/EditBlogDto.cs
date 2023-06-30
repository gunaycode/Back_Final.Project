using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BlogDto
{
    public class EditBlogDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TextAll { get; set; } = null!;
        public DateTime Date { get; set; }
        
    }
}
