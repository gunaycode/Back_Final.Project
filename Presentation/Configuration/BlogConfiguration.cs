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
    public class BlogConfiguration: IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
            builder.HasKey(h => h.Id);
            builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(600);

            builder.Property(b => b.TextAll)
                .IsRequired();

            builder.Property(d => d.FAQs)
           .IsRequired();

        }
        
    }
}
