using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Extantion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configuration
{
    public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels"); 

            builder.HasKey(h => h.Id); 
            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(h => h.Rating).IsRequired();
            
            builder.HasOne(h => h.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h=>h.CityId)
                .IsRequired();

            builder.HasMany(h=>h.Rooms)
             .WithOne(r => r.Hotel)
            .HasForeignKey(r => r.HotelId);

            builder.Property(h => h.WiFi)
           .IsRequired();

            builder.Property(h => h.Pool)
                .IsRequired();

            builder.Property(h => h.Parking)
                .IsRequired();

            builder.Property(h => h.Location)
                .IsRequired();

            builder.Property(h => h.Breakfast)
                .IsRequired();

            builder.Property(h => h.Pet)
                .IsRequired();
        }


    }


}
