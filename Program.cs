using InvestmentPortfolioAPI.Data;
using InvestmentPortfolioAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // הגדרה של ApplicationDbContext עם חיבור למסד נתונים
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // הוספת שירותי ה-API
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IImaggaService, ImaggaService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
