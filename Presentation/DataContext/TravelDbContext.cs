using Application.DTOs.CountryDto;
using Application.DTOs.HotelDto;
using Application.DTOs.SearchDto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataContext
{
    public class TravelDbContext:IdentityDbContext<User, Role, int>
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }   
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<Hotel>Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ImageHotel> ImagesHotel { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ImageRoom> ImagesRoom { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ImageBlog> ImagesBlog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

    }
}
