using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Building;
using Building.Domain.Entities.Contact;
using Building.Domain.Entities.IncomeAndExpenditure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.DbSetting
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<ApartmentExpense> ApartmentExpenses { get; set; }
        public DbSet<Case> Cases { get; set; }


       
    }
}
