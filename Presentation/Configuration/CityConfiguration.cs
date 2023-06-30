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
    public class CityConfiguration: IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities"); 

            builder.HasKey(c => c.Id); 
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100); 

            builder.HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
