using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blog:BaseAuditable
    {
        public Blog()
        {
            Images = new HashSet<ImageBlog>();
        }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TextAll { get; set; } = null!;
        public string FAQs { get; set; } = null!;
        public ICollection<ImageBlog> Images { get; set; }

    }
}
