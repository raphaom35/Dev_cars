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
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.id);
            //modelBuilder.Entity<Car>().ToTable("tb_Car");
            builder.Property(c => c.Brand).HasMaxLength(100);
            //builder.Property(c => c.Brand).HasDefaultValue("Marca");
            builder.Property(c => c.ProdutionDate).HasDefaultValueSql("getdate()");
        }
    }
}
