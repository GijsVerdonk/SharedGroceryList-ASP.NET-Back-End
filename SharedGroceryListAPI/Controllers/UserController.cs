using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedGroceryListAPI.Context;
using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DBContext_SGL _context;

        public UserController(DBContext_SGL context)
        {
            _context = context;
        }
        
        [HttpGet("GetAuth0Id")]
        public IActionResult Test()
        {
            UserSevice userSevice = new UserSevice();
            string accessToken = Request.Cookies["accessToken"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                return Ok(userSevice.GetAuth0IdFromCookie(accessToken));
            }
            return Ok("Access token not found in cookie.");
        }
        
        // GET: api/Lists
        [HttpGet("Lists")]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            string userSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Sub == userSub);

            if (user == null)
            {
                return NotFound("User not found.");
            }
            
            var listData = await _context.UserLists
                .Where(ul => ul.UserId == user.Id)
                .Join(_context.Lists,
                    ul => ul.ListId,
                    l => l.Id,
                    (ul, l) => l)
                .ToListAsync();

            if (listData == null || user == null)
            {
                return NotFound();
            }
            
            return listData;
        }

        // GET: api/UserControlleer
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }
        
        // GET: api/UserControlleer
        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            string userSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var searchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Sub == userSub);

            if (searchedUser == null)
            {
                return NotFound();
            }
            return searchedUser;
        }

        // GET: api/UserControlleer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/UserControlleer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/UserControlleer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string userSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

              if (_context.Users == null)
              {
                  return Problem("Entity set 'DBContext_SGL.Users'  is null.");
              }

            user.Sub = userSub;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/UserControlleer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
