using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.CategoryName).WithMany(c => c.Rooms).HasForeignKey(c => c.CategoryNameId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Price)
                .IsRequired();

            builder.Property(c => c.Count)
                .IsRequired()
                .HasMaxLength(4);

            builder.HasMany(r => r.Reservations)
                .WithOne(reservation => reservation.Room)
                .HasForeignKey(reservation => reservation.RoomId);

        }
    }
}
