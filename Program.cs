
using FluentAPI.Data;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace FluentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FluentAPIContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();

            // L�gger till Rate Limiting i services f�r att begr�nsa antalet till�tna f�rfr�gningar.
            // Konfigurerar en 'Fixed Window' rate limiter med specifika inst�llningar.
            builder.Services.AddRateLimiter(options =>
            {
                // Skapar en limiter med fast tidsf�nster, kallad "Fixed".
                options.AddFixedWindowLimiter("Fixed", limiterOptions =>
                {
                    // Till�ter endast 1 f�rfr�gan inom varje 50-sekundersf�nster.
                    limiterOptions.PermitLimit = 1;

                    // Anger tidsf�nstret till 50 sekunder, vilket betyder att varje ny till�ten
                    // f�rfr�gan kan skickas f�rst efter att 50 sekunder har g�tt.
                    limiterOptions.Window = TimeSpan.FromSeconds(50);

                    // Till�ter upp till 2 f�rfr�gningar i k� om gr�nsen �r n�dd.
                    limiterOptions.QueueLimit = 2;

                    // Behandlar k�ade f�rfr�gningar i den ordning de kom in (�ldsta f�rst).
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                // Returnerar HTTP-statuskod 429 (Too Many Requests) n�r gr�nsen �verskrids.
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Aktiverar Rate Limiting middleware i applikationen.
            app.UseRateLimiter();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
