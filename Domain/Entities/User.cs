using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class User:IdentityUser<int>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}

