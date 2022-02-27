using Building.Domain.Entities.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.DatabaseMapping.FluentApi.Messages
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Content).IsRequired();

            #region ForeingKey

            entity.HasOne(x => x.User)
              .WithMany(x => x.Messages)
              .HasForeignKey(x => x.UserId);

            #endregion

       
        }
    }
}
