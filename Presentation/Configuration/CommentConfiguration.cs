using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Extantion;
using Microsoft.Extensions.Configuration;

namespace Persistance.Configuration
{
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        private readonly IConfiguration _configuration; 

        public CommentConfigurations(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments"); 

            builder.HasKey(c => c.Id); 

            builder.Property(c => c.Text)
                .IsRequired();
            builder.Property(r => r.Rating)
              .HasAnnotation("MinValue", _configuration.GetValue<int>("AppSettings:RatingMinValue"))
              .HasAnnotation("MaxValue", _configuration.GetValue<int>("AppSettings:RatingMaxValue"));       

            builder.HasOne(c => c.User)
                .WithMany(c=>c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CommentLikes)
                .WithOne(cl => cl.Comment)
                .HasForeignKey(cl => cl.CommentId) 
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Hotel)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }



}
