using Devlance.Application.Services;
using Devlance.Domain.Helpers;
using Devlance.Domain.Interfaces.Repositories;
using Devlance.Domain.Interfaces.Services;
using Devlance.Domain.Models;
using Devlance.Infrastructure.DbContext;
using Devlance.Infrastructure.Repositories;
using Devlance.Infrastructure.SystemStartupData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Devlance_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DevlanceContext>();
            builder.Services.AddDbContext<DevlanceContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			}
			);


			/*JWT Configuration*/
			builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
			builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });


            //Register Repository
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped(typeof(IFreelancerProfileRepository), typeof(FreelancerProfileRepository));


            //Register Services
            builder.Services.AddScoped<IFreelancerProfileService,FreelancerProfileService>();
            builder.Services.AddScoped<IAuthService,AuthService>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //Seed Data for updating database with un applied migrations
            app.UseData();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
