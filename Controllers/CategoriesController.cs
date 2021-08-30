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
    public class CategoriesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public CategoriesController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Categories //get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5 //get category specified by an id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var category = await _context.Categories.FindAsync(Id);

                if (category == null)
                {
                    return NotFound();
                }

                return category;
            }
            catch (FormatException e)
            {
                return BadRequest();
            }
        }

        // PUT: api/Categories/5 //modify category specified by an id

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(String id, Category category)
        {
            try
            {
                int Id = int.Parse(id);
                if (Id != category.Id || category.Name == null || category.Description == null)
                {
                    return BadRequest();
                }

                _context.Entry(category).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(Id))
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

        // POST: api/Categories //create a new category

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (category.Name == null || category.Description == null)
            {
                return BadRequest();
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5 //delete a category specified by an id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(String id)
        {
            try
            {
                int Id = int.Parse(id);
                var category = await _context.Categories.FindAsync(Id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (FormatException e)
            {
                return BadRequest();
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }

}