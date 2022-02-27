using ApartmentManagment.MvcHelper.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace ApartmentManagmentWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews().AddFluentValidation(
                opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining(typeof(Startup));
                }); ;

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {


            }
            app.UseWhen(
                    context => context != null,
                    appBuilder =>
                    {
                        appBuilder.UseExceptionHandler("/Error/500");
                        appBuilder.UseStatusCodePagesWithReExecute("/Error/{0}");
                    }
                );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ActionResultExtensions.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>(),
              app.ApplicationServices.GetRequiredService<ITempDataDictionaryFactory>());
        }
    }
}
