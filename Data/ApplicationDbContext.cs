namespace InvestmentPortfolioAPI.Data;

using InvestmentPortfolioAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationDbContext : DbContext
{
    public DbSet<PortfolioItem> PortfolioItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // הגדרות נוספות של המודל במידת הצורך
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // הוסף הגדרות קונפיגורציה נוספות כאן
    }
}

