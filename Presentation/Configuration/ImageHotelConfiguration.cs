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
    public class ImageHotelConfiguration: IEntityTypeConfiguration<ImageHotel>
    {
        public void Configure(EntityTypeBuilder<ImageHotel> builder)
        {

            builder.Property(i => i.ImageName)
                .IsRequired()
                .HasMaxLength(1100); 

            
        }
    }
}
