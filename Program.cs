using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lägg till CORS-tjänsten
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") //Temporärt.
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            //Configure services
            builder.Services.AddDbContext<ToDoContext>(options =>
                options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection")));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddControllers(); 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");//CORS-policyn // Detta måste vara här innan app.UseAuthorization()
            app.UseAuthorization();

            app.MapControllers(); // Koppla controller endpoints

            app.Run();
        }
    }
}
