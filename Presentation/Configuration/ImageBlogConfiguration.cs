using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configuration
{
    public class ImageBlogConfiguration: IEntityTypeConfiguration<ImageBlog>
    {
        public void Configure(EntityTypeBuilder<ImageBlog> builder)
        {
            
            builder.HasKey(c => c.Id);
            builder.Property(i => i.ImageName)
                .IsRequired()
                .HasMaxLength(1100);

            builder.Property(h => h.Path)
            .IsRequired();

        }
        

    }
}
