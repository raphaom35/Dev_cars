using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Presistence.Configurations
{
    public class CustumerDbConfiguration : IEntityTypeConfiguration<Custumer>
    {
        public void Configure(EntityTypeBuilder<Custumer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Custumer)
                .HasForeignKey(o => o.IdCurtumer)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
