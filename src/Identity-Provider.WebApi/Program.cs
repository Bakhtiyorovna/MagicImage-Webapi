﻿
using Identity_Provider.WebApi.Configurations;
using Identity_Provider.WebApi.Configurations.Layers;
using Identity_Provider.WebApi.Middlewares;

namespace Identity_Provider.WebApi
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

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();

            builder.ConfigureCORSPolicy();



            builder.ConfigureJwtAuth();
            builder.ConfigureSwaggerAuth();
            builder.ConfigureDataAccess();
            builder.ConfigureServiceLayer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseStaticFiles();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


//using Identity_Provider.WebApi.Configurations;
//using Identity_Provider.WebApi.Configurations.Layers;
//using Identity_Provider.WebApi.Middlewares;



//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddMemoryCache();

////builder.ConfiguraWeb();
//builder.ConfigureDataAccess();
//builder.ConfigureServiceLayer();

//var app = builder.Build();
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();
//app.UseCors("AllowAll");
//app.UseStaticFiles();
//app.UseMiddleware<ExceptionHandlerMiddleware>();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();