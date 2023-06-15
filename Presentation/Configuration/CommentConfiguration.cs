using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Extantion;

namespace Persistance.Configuration
{
    public class CommentConfigurations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments"); 

            builder.HasKey(c => c.Id); 

            builder.Property(c => c.Text)
                .IsRequired(); 

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId); 

            builder.HasMany(c => c.CommentLikes)
                .WithOne(cl => cl.Comment)
                .HasForeignKey(cl => cl.CommentId); 

            
        }

    }



}
