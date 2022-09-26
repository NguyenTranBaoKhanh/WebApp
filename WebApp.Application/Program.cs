using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace WebApp.Application
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

            // Add front-end route for SPA middle ware
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "frontend/build";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // React static middleware
            app.UseSpaStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            // React start app route
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "frontend";

                if (app.Environment.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.Run();
        }
    }
}