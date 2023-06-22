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
    public class RoomConfiguratin : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            

            builder.Property(r => r.RoomName)
                .IsRequired();

            builder.Property(r => r.Price)
                .IsRequired();

            builder.HasMany(r => r.Reservations)
                .WithOne(reservation => reservation.Room)
                .HasForeignKey(reservation => reservation.RoomId);

        }
    }
}
