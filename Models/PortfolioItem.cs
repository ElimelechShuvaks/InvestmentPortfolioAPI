using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolioAPI.Models;

public class PortfolioItem
{
    public int Id { get; set; }
    public string StockName { get; set; }    // שם המניה
    public string Symbol { get; set; }       // סימול המניה
    public int Quantity { get; set; }        // כמות
    public decimal PurchasePrice { get; set; } // מחיר רכישה
    public decimal CurrentPrice { get; set; }  // מחיר נוכחי
    public decimal TotalValue { get; set; }    // ערך כולל
}


//public class PortfolioItem
//{
//    public int Id { get; set; }
//    [Required]
//    public string StockName { get; set; }
//    [Required]
//    public string Symbol { get; set; }
//    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
//    public int Quantity { get; set; }
//    [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be positive.")]
//    public decimal PurchasePrice { get; set; }
//    public decimal CurrentPrice { get; set; }
//    public decimal TotalValue => Quantity * CurrentPrice;
//}

