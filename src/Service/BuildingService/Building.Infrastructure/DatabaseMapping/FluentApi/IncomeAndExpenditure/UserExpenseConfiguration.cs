using Building.Domain.Entities.IncomeAndExpenditure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.DatabaseMapping.FluentApi.IncomeAndExpenditure
{
    public class UserExpenseConfiguration : IEntityTypeConfiguration<ApartmentExpense>
    {
        public void Configure(EntityTypeBuilder<ApartmentExpense> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Bill).IsRequired();

            #region ForeingKey

            entity.HasOne(x => x.User)
              .WithMany(x => x.ApartmentExpenses)
              .HasForeignKey(x => x.UserId);

            #endregion


        }
    }
}
