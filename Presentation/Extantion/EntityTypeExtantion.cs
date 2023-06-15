using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Extantion
{
    public static class EntityTypeExtantion
    {
       
            public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
                where TEntity : BaseEntity
            {
                builder.Property(c => c.IsDeleted).HasDefaultValue(false);
                builder.Property(c => c.IsActived).HasDefaultValue(true);

            }

            public static void ConfigureAudittable<TEntity>(this EntityTypeBuilder<TEntity> builder)
                where TEntity : BaseAuditable
            {
                builder.Property(c => c.UpdateDate).HasDefaultValueSql("GETUTCDATE()");
                builder.Property(c => c.CreateDate).HasDefaultValueSql("GETUTCDATE()");

            }
        




    }
}
