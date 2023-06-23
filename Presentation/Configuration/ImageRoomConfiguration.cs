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
    public class ImageRoomConfiguration: IEntityTypeConfiguration<ImageRoom>
    {
        public void Configure(EntityTypeBuilder<ImageRoom> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(x => x.Room).WithMany(x => x.RoomImages).HasForeignKey(x => x.RoomId);
            builder.Property(i => i.ImageName)
                .IsRequired()
                .HasMaxLength(1100);

            builder.Property(h => h.Path)
            .IsRequired();
        }
    }
}
