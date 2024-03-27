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
    public class UserListController : ControllerBase
    {
        private readonly DBContext_SGL _context;

        public UserListController(DBContext_SGL context)
        {
            _context = context;
        }

        // GET: api/UserList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetUserLists()
        {
          if (_context.UserLists == null)
          {
              return NotFound();
          }

          List<UserListDto> userListDtos = new List<UserListDto>();
          
          foreach (var userList in _context.UserLists)
          {
              UserListDto newUserListDto = new UserListDto()
              {
                    ListId = userList.ListId,
                    UserId = userList.UserId,
                    IsCreator = userList.IsCreator,
                    IsActive = userList.IsActive
              };
              
              userListDtos.Add(newUserListDto);
          }
          
          return userListDtos;
        }

        // PUT: api/UserList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserList(int id, UserListDto userListDto)
        {
            
            var userList = new UserList()
            {
                ListId = userListDto.ListId,
                UserId = userListDto.UserId,
                IsActive = userListDto.IsActive,
                IsCreator = userListDto.IsCreator
            };

            _context.Entry(userList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UserLists.Any(ul => ul.UserId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/UserList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserList>> PostUserList(UserListDto userListDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userList = new UserList
                {
                    UserId = userListDto.UserId,
                    ListId = userListDto.ListId,
                    IsActive = userListDto.IsActive,
                    IsCreator = userListDto.IsCreator
                };
                
                _context.UserLists.Add(userList);
                
                await _context.SaveChangesAsync();
                
                return userList;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
