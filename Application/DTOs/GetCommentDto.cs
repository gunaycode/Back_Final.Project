using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        
    }
}
