using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace Lyne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IProductService productService,IOrderService orderService,ICategoryService categoryService, IUserService userService) : ControllerBase
    {
        // GET: api/admin/dashboard
        [HttpGet("Dashboard")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public IActionResult GetDashboard()
        {
            return null;
        }

        // GET: api/admin/users
        [HttpGet("Users")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public IActionResult GetUsers()
        {
            return Redirect("/api/Users");
        }
        [HttpGet("User/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public IActionResult GetUser(int id)
        {
            return Redirect($"/api/User/{id}");
        }

        // DELETE: api/admin/user/1
        [HttpDelete("User/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var removedId = await userService.DeleteByIdAsync(id);
           
            return Ok(new
            {
                message = $"User with id:{id} deleted successfully"
            });
        }

        // POST: api/admin/user
        [HttpPost("User/Create")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest("User data is required");

            var created = await userService.AddAsync(user);
            if (!created)
                return BadRequest("Failed to create user");

            return Ok(new
            {
                message = $"User {user.Name} created successfully",
                user
            });
        }

        // PUT: api/admin/user/1
        [HttpPut("User/Update")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest("User data is required");

            var updated = await userService.UpdateAsync(user);
            if (!updated)
                return BadRequest("Failed to update user");

            return Ok(new
            {
                message = $"User {user.Name} updated successfully",
                user
            });
        }
        [HttpGet("Orders")]
        public IActionResult GetOrders()
        {
            return Redirect($"/api/Orders");
        }
        [HttpGet("Items")]
        public IActionResult GetProducts()
        {
            return Redirect($"/api/Products");
        }
        [HttpGet("Filters")]
        public IActionResult GetFilters()
        {
            return null;
        }
        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            return Redirect($"/api/Categories");
        }
    }
}
