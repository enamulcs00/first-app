using first_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace first_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public ProductsController(ProductDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewRecord(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            var obj = new { Message = "Data has been added successfully", Data = product };

            return Ok(obj);
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllRecord() {
            return await _context.Products.ToListAsync();
                
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneRecord(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneRecord(Product productData) {

            if (productData == null || productData.Id == 0)
                return BadRequest();

            var product = await _context.Products.FindAsync(productData.Id);
            if (product == null)
                return NotFound();
            product.Name = productData.Name;
            product.Description = productData.Description;
            product.Price = productData.Price;
            await _context.SaveChangesAsync();
            var obj = new { Message = "Data has been updated successfully", Data = product };

            return Ok(obj);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneRecord(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }
    }
}
