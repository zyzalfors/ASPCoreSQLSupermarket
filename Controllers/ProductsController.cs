using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Models;

namespace Supermarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDataContext _context;

        public ProductsController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Products 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(String name ="")
        {
            IQueryable<Product> query = _context.Products.AsQueryable();
            if (name != "")
            {
                query = query.Where<Product>(x => x.Name.ToLower().Contains(name.ToLower()));
            }

            return await query.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var product = await _context.Products.FindAsync(Id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (FormatException e)
            {
                return BadRequest();
            }
        }

        // PUT: api/Products/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(String id, Product product)
        {
            try
            {
                int Id = int.Parse(id);
                if (Id != product.Id || product.Name == null)
                {
                    return BadRequest();
                }

                _context.Entry(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (FormatException e)
            {
                return BadRequest();
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product.Name == null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var product = await _context.Products.FindAsync(Id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (FormatException e)
            {
                return BadRequest();
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}