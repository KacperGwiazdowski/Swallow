using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Repository;
using Swallow.Core.Services;
using Swallow.DataAccessLayer;
using Swallow.DataCollector.Gis;
using System.Security.Claims;
using System.Text;

namespace Swallow.WebApi
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
            string passwordKey = Configuration.GetValue<string>("PasswordServiceKey");


            services.AddControllers();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<MeasurmentStation, int>, MeasurmentStationRepository>();
            services.AddScoped<IRepository<Sensor, int>, SensorRepository>();
            services.AddScoped<IDataMeasurmentRepository, DataMeasurmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDataCollectionService, DataCollectionService>();
            services.AddScoped<IDataCollector>(sp => new GisDataCollector(Configuration.GetValue<string>("GiosBaseUrl")));


            services.AddDbContext<SwallowDataDbContext>(o =>
                o.UseLazyLoadingProxies()
                .UseSqlServer("Server=.;Database=SwallowDataDB;Trusted_Connection=True;"));
            //.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=SwallowDataDB;Pooling=true;"));



            services.AddCors(options => options.AddPolicy("AllowLocalhost", builder =>
            {
                builder
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins("http://localhost:8081", "http://localhost:8080", "http://localhost")
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                builder.AllowAnyMethod();
                builder.WithHeaders(HeaderNames.ContentType,
                    HeaderNames.Authorization);
            }
            ));
            services
                .AddAuthentication(x =>
                 {
                     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(passwordKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization(
                auth =>
                {
                    auth.AddPolicy("RequireAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("AllowLocalhost");
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
