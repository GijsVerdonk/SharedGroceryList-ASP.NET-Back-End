using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedGroceryListAPI.Context;
using SharedGroceryListAPI.Dtos;
using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemController : ControllerBase
    {
        private readonly DBContext_SGL _context;

        public ListItemController(DBContext_SGL context)
        {
            _context = context;
        }

        // GET: api/ListItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListItemDto>>> GetListItems()
        {
            if (_context.ListItems == null)
            {
                return NotFound();
            }

            List<ListItemDto> itemListDtos = new List<ListItemDto>();

            foreach (var listItem in _context.ListItems)
            {
                ListItemDto newListItemDto = new ListItemDto()
                {

                    ListId = listItem.ListId,
                    ItemId = listItem.ItemId,
                    Quantity = listItem.Quantity,
                    IsActive = listItem.IsActive
                };

                itemListDtos.Add(newListItemDto);
            }

            return itemListDtos;
        }

        // PUT: api/ListItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutListItem(ListItemDto listItemDto)
        {
            var listItem = new ListItem()
            {
                ListId = listItemDto.ListId,
                ItemId = listItemDto.ItemId,
                IsActive = listItemDto.IsActive,
                Quantity = listItemDto.Quantity
            };

            _context.Entry(listItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ListItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListItem>> PostListItem(ListItemDto listItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map DTO to entity model
                var listItem = new ListItem
                {
                    ListId = listItemDto.ListId,
                    ItemId = listItemDto.ItemId,
                    Quantity = listItemDto.Quantity,
                    IsActive = listItemDto.IsActive
                };

                // Add UserList to DbContext
                _context.ListItems.Add(listItem);

                // Save changes to persist the new UserList
                await _context.SaveChangesAsync();

                // Return the created UserList
                return listItem;
            }
            catch (Exception ex)
            {
                // Log or handle any errors
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
