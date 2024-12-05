using System;
using System.Threading.Tasks;
using InvestmentPortfolioAPI.Data;
using InvestmentPortfolioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioAPI.Controllers
{
    /// <summary>
    /// Controller for managing portfolio items.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PortfolioController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioController"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing portfolio data.</param>
        /// <param name="logger">Logger instance for logging information and errors.</param>
        public PortfolioController(ApplicationDbContext context, ILogger<PortfolioController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all portfolio items.
        /// </summary>
        /// <returns>A list of all portfolio items.</returns>
        /// <response code="200">Returns the list of portfolio items.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all portfolio items.");

            try
            {
                var items = await _context.PortfolioItems.ToListAsync();
                _logger.LogInformation("Successfully retrieved {Count} portfolio items.", items.Count);
                return Ok(items);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while fetching portfolio items.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database service is unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching portfolio items.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Retrieves a specific portfolio item by its ID.
        /// </summary>
        /// <param name="id">The ID of the portfolio item.</param>
        /// <returns>The portfolio item with the specified ID.</returns>
        /// <response code="200">Returns the requested portfolio item.</response>
        /// <response code="404">If the portfolio item is not found.</response>
        /// <response code="500">If an unexpected error occurs while fetching data.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching portfolio item with ID {Id}.", id);

            try
            {
                var item = await _context.PortfolioItems.FindAsync(id);
                if (item == null)
                {
                    _logger.LogWarning("Portfolio item with ID {Id} not found.", id);
                    return NotFound("Portfolio item not found.");
                }

                _logger.LogInformation("Successfully retrieved portfolio item with ID {Id}.", id);
                return Ok(item);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while fetching portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database service is unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Creates a new portfolio item.
        /// </summary>
        /// <param name="item">The portfolio item to create.</param>
        /// <returns>The newly created portfolio item.</returns>
        /// <response code="201">Returns the newly created portfolio item.</response>
        /// <response code="400">If the portfolio item is null or invalid.</response>
        /// <response code="500">If an unexpected error occurs while creating the item.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PortfolioItem item)
        {
            _logger.LogInformation("Creating a new portfolio item.");

            try
            {
                if (item == null)
                {
                    _logger.LogWarning("Create action received a null portfolio item.");
                    return BadRequest("Portfolio item is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Create action received an invalid portfolio item.");
                    return BadRequest("Invalid portfolio item.");
                }

                // Additional logic can be added here, such as updating current price
                _context.PortfolioItems.Add(item);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully created portfolio item with ID {Id}.", item.Id);
                return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while creating a new portfolio item.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database service is unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a new portfolio item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Updates an existing portfolio item.
        /// </summary>
        /// <param name="id">The ID of the portfolio item to update.</param>
        /// <param name="updatedItem">The updated portfolio item data.</param>
        /// <returns>The updated portfolio item.</returns>
        /// <response code="200">Returns the updated portfolio item.</response>
        /// <response code="400">If the input data is invalid.</response>
        /// <response code="404">If the portfolio item is not found.</response>
        /// <response code="500">If an unexpected error occurs while updating the item.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] PortfolioItem updatedItem)
        {
            _logger.LogInformation("Updating portfolio item with ID {Id}.", id);

            try
            {
                if (updatedItem == null || id != updatedItem.Id)
                {
                    _logger.LogWarning("Update action received invalid portfolio item data for ID {Id}.", id);
                    return BadRequest("Invalid portfolio item data.");
                }

                var existingItem = await _context.PortfolioItems.FindAsync(id);
                if (existingItem == null)
                {
                    _logger.LogWarning("Portfolio item with ID {Id} not found for update.", id);
                    return NotFound("Portfolio item not found.");
                }

                // Update the necessary fields
                existingItem.StockName = updatedItem.StockName;
                existingItem.Symbol = updatedItem.Symbol;
                existingItem.Quantity = updatedItem.Quantity;
                existingItem.PurchasePrice = updatedItem.PurchasePrice;
                existingItem.CurrentPrice = updatedItem.CurrentPrice;

                _context.PortfolioItems.Update(existingItem);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully updated portfolio item with ID {Id}.", id);
                return Ok(existingItem);
            }
            catch (DbUpdateConcurrencyException dbCcEx)
            {
                _logger.LogError(dbCcEx, "Concurrency error while updating portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status409Conflict, "Concurrency conflict occurred. Please try again.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while updating portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database service is unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }

        /// <summary>
        /// Deletes a specific portfolio item by its ID.
        /// </summary>
        /// <param name="id">The ID of the portfolio item to delete.</param>
        /// <returns>A confirmation message upon successful deletion.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="404">If the portfolio item is not found.</response>
        /// <response code="500">If an unexpected error occurs while deleting the item.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting portfolio item with ID {Id}.", id);

            try
            {
                var item = await _context.PortfolioItems.FindAsync(id);
                if (item == null)
                {
                    _logger.LogWarning("Portfolio item with ID {Id} not found for deletion.", id);
                    return NotFound("Portfolio item not found.");
                }

                _context.PortfolioItems.Remove(item);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted portfolio item with ID {Id}.", id);
                return Ok(new { message = "Portfolio item deleted successfully." });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while deleting portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database service is unavailable. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting portfolio item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }
}
