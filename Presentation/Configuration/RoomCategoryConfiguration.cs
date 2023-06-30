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
    public class RoomCategoryConfiguration: IEntityTypeConfiguration<RoomCategory>
    {
        public void Configure(EntityTypeBuilder<RoomCategory> builder)
        {

            builder.HasKey(c => c.Id);
            builder.HasMany(c=>c.Rooms)
                .WithOne(c=>c.CategoryName)
                .HasForeignKey(c=>c.CategoryNameId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
