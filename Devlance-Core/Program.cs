
using Devlance.Domain.Models;
using Devlance.Infrastructure.DbContext;
using Devlance.Infrastructure.SystemStartupData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //Seed Data
            app.UseData();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
