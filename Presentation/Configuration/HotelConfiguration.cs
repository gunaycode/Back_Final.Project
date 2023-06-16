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

            builder.Property(h => h.Price)
                .HasColumnType("decimal(18, 2)");

            builder.HasMany(h => h.Cities)
                .WithMany(c => c.Hotels);
        }


    }


}
