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

            // הוספת שירותים למכולת ה-Dependency Injection

            // הגדרת ApplicationDbContext עם חיבור למסד הנתונים
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // רישום שירותי ה-API
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IHistoricalDataService, HistoricalDataService>(); 
            builder.Services.AddScoped<IMarketDataService, MarketDataService>();
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<ISearchService, SearchService>();

            // הוספת בקרי ה-API
            builder.Services.AddControllers();

            // הוספת Swagger לצורך תיעוד ובדיקת ה-API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // הגדרת צנרת הבקשות (HTTP request pipeline)
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
