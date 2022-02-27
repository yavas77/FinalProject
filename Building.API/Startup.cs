using Building.Application.CustomConfigurations;
using Building.Application.Features.Commands.Authentications.RegisterUser;
using Building.Application.Model.Settings;
using Building.Domain.Entities.Authentications;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Building.Infrastructure.ServiceConfigurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Building.API
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
            services.AddApplicationServices();

            services.AddInfrastructureServices(Configuration);

            #region Settings

            services.Configure<JwtSetting>(Configuration.GetSection("JWT"));
            var jwt = Configuration.GetSection("JWT").Get<JwtSetting>();


            #endregion

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.ConfigureCors();

            services.AddControllers().AddFluentValidation(
                opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining(typeof(RegisterUserCommand));
                });

            #region JWT 

            services.AddAuth(jwt);

            #endregion

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building.API", Version = "v1" });
            //});

            #region Swagger 
            string version = "v1";
            services.AddSwaggerGen(gen =>
            {
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Jwt Bearer Token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    },
                };

                gen.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = "Apartment Managetment Wep Api",
                    Version = version,
                    License = new OpenApiLicense
                    {
                        Name = "Apartment Managetment",
                        Url = new Uri("https://omeryavas.com/"),
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Ömer YAVAÞ",
                        Email = "omeryavas@msn.com"
                    }
                });

                gen.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                gen.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
                gen.UseAllOfToExtendReferenceSchemas();
                //gen.IncludeXmlCommentsFromInheritDocs(includeRemarks: true, excludedTypes: typeof(string));
                //gen.AddEnumsWithValuesFixFilters(services, o =>
                //{
                //    // add schema filter to fix enums (add 'x-enumNames' for NSwag) in schema
                //    o.ApplySchemaFilter = true;

                //    // add parameter filter to fix enums (add 'x-enumNames' for NSwag) in schema parameters
                //    o.ApplyParameterFilter = true;

                //    // add document filter to fix enums displaying in swagger document
                //    o.ApplyDocumentFilter = true;

                //    // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' for schema extensions) for applied filters
                //    o.IncludeDescriptions = true;

                //    // add remarks for descriptions from xml-comments
                //    o.IncludeXEnumRemarks = true;

                //    // get descriptions from DescriptionAttribute then from xml-comments
                //    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;


                //});
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireJobberRole",
                     policy => policy.RequireRole("Jobber"));
            });

            #endregion

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireJobberRole",
                     policy => policy.RequireRole("Jobber"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Building.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
