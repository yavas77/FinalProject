using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.DatabaseMapping.FluentApi.Authentications
{
    public class AuthorizationConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(x => x.TC).IsRequired();

            #region Index

            entity.HasIndex(e => new { e.TC }, "UIX_TC").IsUnique();
            entity.HasIndex(e => new { e.Plate }, "UIX_Plate").IsUnique();

            #endregion
        }
    }
}
