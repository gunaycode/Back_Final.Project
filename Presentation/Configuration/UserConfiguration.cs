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
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.BirthDate).HasColumnType("datetime");

        }
    }
}
