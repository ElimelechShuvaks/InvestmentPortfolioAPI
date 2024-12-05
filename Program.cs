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

            // רישום HttpClient לשירותי DI
            builder.Services.AddHttpClient();

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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Investment Portfolio API",
                    Version = "v1",
                    Description = "API for managing investment portfolios, retrieving stock information, and more.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your.email@example.com"
                    }
                });

                // הוספת קובץ תיעוד XML אם קיים
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            var app = builder.Build();

            // הגדרת צנרת הבקשות (HTTP request pipeline)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Investment Portfolio API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
