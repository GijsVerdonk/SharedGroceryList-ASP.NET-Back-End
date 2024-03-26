using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedGroceryListAPI.Context;
using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly DBContext_SGL _context;

        public ListController(DBContext_SGL context)
        {
            _context = context;
        }

        // GET: api/List
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
          if (_context.Lists == null)
          {
              return NotFound();
          }
            return await _context.Lists.ToListAsync();
        }

        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
          if (_context.Lists == null)
          {
              return NotFound();
          }
            var list = await _context.Lists.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/List/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, List list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
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

        // POST: api/List
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List>> PostList(List list)
        {
          if (_context.Lists == null)
          {
              return Problem("Entity set 'DBContext_SGL.Lists'  is null.");
          }
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/List/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            if (_context.Lists == null)
            {
                return NotFound();
            }
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return (_context.Lists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
