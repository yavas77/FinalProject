using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Infrastructure.Contracts.Persistence.Repositories.Common;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Building.Application.Contracts.Persistence.Repositories.Buildings;
using Building.Infrastructure.Contracts.Persistence.Repositories.Buildings;
using Building.Application.Services.Buildings;
using Building.Infrastructure.Services.Building;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Infrastructure.Services.IncomeAndExpenditure;
using Building.Application.Contracts.Persistence.Repositories.IncomeAndExpenditure;
using Building.Infrastructure.Contracts.Persistence.Repositories.IncomeAndExpenditure;
using Building.Application.Services.Messages;
using Building.Infrastructure.Services.Messages;
using Building.Application.Contracts.Persistence.Repositories.Messages;
using Building.Infrastructure.Contracts.Persistence.Repositories.Messages;

namespace Building.Infrastructure.ServiceConfigurations
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BuildingConnectionString")));

            #region Authentications

            #endregion


            #region Commons

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            #endregion

            #region Caching

            //  services.AddTransient<ICacheService, MemoryCacheService>();

            #endregion


            #region Buildings

            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IBlockService, Blockservice>();

            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IApartmentService, ApartmentService>();

            #endregion


            #region IncomeAndExpenditure

            services.AddScoped<IApartmentExpenseService, ApartmentExpenseService>();
            services.AddScoped<IApartmentExpenseRepository, ApartmentExpenseRepository>();

            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<ICaseRepository, CaseRepository>();

            #endregion


            #region Messages

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            #endregion

            return services;
        }
    }
}