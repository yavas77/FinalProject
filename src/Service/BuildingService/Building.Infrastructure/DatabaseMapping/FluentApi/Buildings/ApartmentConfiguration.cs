using Building.Domain.Entities.Building;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.DatabaseMapping.FluentApi.Buildings
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x=>x.ApartmentType).IsRequired();
            entity.Property(x=>x.No).IsRequired();

            #region ForeingKey

            entity.HasOne(x => x.Block)
              .WithMany(x => x.Apartments)
              .HasForeignKey(x => x.BlockId);

            #endregion
        }
    }
}
