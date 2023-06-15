using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommentLike
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
        public int UserId { get; set; } 
        public User User { get; set; } = null!;
    }
}
