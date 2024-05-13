using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SharedGroceryListAPI.Context;
using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DBContext_SGL _context;

        public UserController(DBContext_SGL context)
        {
            _context = context;
        }
        
        // GET: api/Lists
        [HttpGet("User/Lists")]
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
                .Where(ul => ul.IsActive == true)
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
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }
        
        // GET: api/UserControlleer
        [HttpGet("User")]
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

       // POST: api/UserControlleer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("User")]
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
    }
}
