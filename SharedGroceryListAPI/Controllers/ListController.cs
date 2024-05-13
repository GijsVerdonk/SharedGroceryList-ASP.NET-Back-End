using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
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
        
        [HttpPost("{listId}/Items/{quantity}")]
        public async Task<ActionResult<List>> PostItemToList(int listId, Item item, string quantity)
        {
            //TODO: add security that the list belongs to the user.
            var userSubClaim = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userSubClaim == null)
            {
                Problem("User is not logged in.");
            }

            string userSub = userSubClaim.Value;
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Sub == userSub);

            if (user == null)
            {
                return Problem("User does not exists.");
            }

            var list = await _context.Lists.FirstOrDefaultAsync(l=>l.Id == listId);

            if (list == null)
            {
                return Problem("List not found.");
            }

            item.IsActive = true;
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            
            var listItem = new ListItem()
            {
                ListId = list.Id,
                ItemId = item.Id,
                Quantity = quantity,
                IsActive = true
            };
            
            _context.ListItems.Add(listItem);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            
            if (list == null)
            {
                return NotFound();
            }

            list.IsActive = false;
            _context.Entry(list).State = EntityState.Modified;

            var userLists = await _context.UserLists.Where(ul => ul.ListId == id).ToListAsync();

            if (userLists.Count > 0)
            {
                foreach (var userList in userLists)
                {
                    userList.IsActive = false;
                    _context.Entry(userList).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{listId}/Items/{itemId}")]
        public async Task<IActionResult> DeleteItemFromList(int listId, int itemId)
        {
            var list = await _context.Lists.FindAsync(listId);
            
            if (list == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems.FirstOrDefaultAsync(li => li.ItemId == itemId);

            if (listItem == null)
            {
                return Problem("Item not connected to the list.");
            }
            
            listItem.IsActive = false;
            
            _context.Entry(listItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/List
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List>> PostList(List list)
        {
            string userSub = User.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Sub == userSub);
            
          if (_context.Lists == null)
          {
              return Problem("Entity set 'DBContext_SGL.Lists'  is null.");
          }

          list.IsActive = true;  
          _context.Lists.Add(list);
          await _context.SaveChangesAsync();
          
            var userList = new UserList
            {
                UserId = user.Id,
                ListId = list.Id,
                IsActive = true,
                IsCreator = true
            };
            
           
            _context.UserLists.Add(userList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // GET: api/UserControlleer
        [HttpGet("{listId}/Items")]
        public async Task<ActionResult<List<Item>>> GetListItems(int listId)
        {
            var list = await _context.Lists.FirstOrDefaultAsync(l => l.Id == listId);

            if (list == null)
            {
                return Problem("List not found.");
            }

            var listItems = await _context.ListItems
                .Where(li => li.ListId == listId)
                .Where(li=>li.IsActive == true)
                .Select(li => li.Item)
                .ToListAsync();

            return listItems;
        }
    }
}
