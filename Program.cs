
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

            // Lägger till Rate Limiting i services för att begränsa antalet tillåtna förfrågningar.
            // Konfigurerar en 'Fixed Window' rate limiter med specifika inställningar.
            builder.Services.AddRateLimiter(options =>
            {
                // Skapar en limiter med fast tidsfönster, kallad "Fixed".
                options.AddFixedWindowLimiter("Fixed", limiterOptions =>
                {
                    // Tillåter endast 1 förfrågan inom varje 50-sekundersfönster.
                    limiterOptions.PermitLimit = 1;

                    // Anger tidsfönstret till 50 sekunder, vilket betyder att varje ny tillåten
                    // förfrågan kan skickas först efter att 50 sekunder har gått.
                    limiterOptions.Window = TimeSpan.FromSeconds(50);

                    // Tillåter upp till 2 förfrågningar i kö om gränsen är nådd.
                    limiterOptions.QueueLimit = 2;

                    // Behandlar köade förfrågningar i den ordning de kom in (äldsta först).
                    limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                // Returnerar HTTP-statuskod 429 (Too Many Requests) när gränsen överskrids.
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
