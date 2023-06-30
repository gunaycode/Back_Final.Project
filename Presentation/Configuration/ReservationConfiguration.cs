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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(c => c.Id);

            builder.Property(d=>d.Date)
                .IsRequired();

            builder.HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomId)
                .IsRequired();
            

            builder.HasOne(r => r.User)
                .WithMany(user => user.Reservations)
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder.HasOne(r => r.RoomCategory)
            .WithMany(category => category.Reservations)
            .HasForeignKey(r => r.RoomCategoryId)
            .IsRequired();

            builder.Property(r => r.Count)
                .IsRequired();

        }
    }
}
