using Microsoft.AspNetCore.Http;
using InvestmentPortfolioAPI.Data;
using InvestmentPortfolioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PortfolioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/portfolio
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var items = await _context.PortfolioItems.ToListAsync();
            return Ok(items);
        }
        catch (Exception ex)
        {
            // לוג שגיאות
            // Log.Error(ex, "Error fetching portfolio items.");
            return StatusCode(500, "Internal server error.");
        }
    }

    // GET: api/portfolio/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var item = await _context.PortfolioItems.FindAsync(id);
            if (item == null)
                return NotFound("Portfolio item not found.");

            return Ok(item);
        }
        catch (Exception ex)
        {
            // Log.Error(ex, $"Error fetching portfolio item with id {id}.");
            return StatusCode(500, "Internal server error.");
        }
    }

    // POST: api/portfolio
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PortfolioItem item)
    {
        try
        {
            if (item == null)
                return BadRequest("Portfolio item is null.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid portfolio item.");

            // ניתן להוסיף כאן לוגיקה נוספת, כמו עדכון מחיר נוכחי
            _context.PortfolioItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
        catch (Exception ex)
        {
            // Log.Error(ex, "Error creating portfolio item.");
            return StatusCode(500, "Internal server error.");
        }
    }

    // PUT: api/portfolio/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PortfolioItem updatedItem)
    {
        try
        {
            if (updatedItem == null || id != updatedItem.Id)
                return BadRequest("Invalid portfolio item data.");

            var existingItem = await _context.PortfolioItems.FindAsync(id);
            if (existingItem == null)
                return NotFound("Portfolio item not found.");

            // עדכון השדות הנדרשים
            existingItem.StockName = updatedItem.StockName;
            existingItem.Symbol = updatedItem.Symbol;
            existingItem.Quantity = updatedItem.Quantity;
            existingItem.PurchasePrice = updatedItem.PurchasePrice;
            existingItem.CurrentPrice = updatedItem.CurrentPrice;

            _context.PortfolioItems.Update(existingItem);
            await _context.SaveChangesAsync();

            return Ok(existingItem);
        }
        catch (Exception ex)
        {
            // Log.Error(ex, $"Error updating portfolio item with id {id}.");
            return StatusCode(500, "Internal server error.");
        }
    }

    // DELETE: api/portfolio/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var item = await _context.PortfolioItems.FindAsync(id);
            if (item == null)
                return NotFound("Portfolio item not found.");

            _context.PortfolioItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Portfolio item deleted successfully." });
        }
        catch (Exception ex)
        {
            // Log.Error(ex, $"Error deleting portfolio item with id {id}.");
            return StatusCode(500, "Internal server error.");
        }
    }
}
