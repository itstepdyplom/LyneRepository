using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lyne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        // GET: api/admin/dashboard
        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            
            return Ok(new
            {
                Title = "Admin Panel",
                TotalUsers = 1240,
                TotalOrders = 438,
                Revenue = 10543.75
            });
        }

        // GET: api/admin/users
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            // Приклад заглушки користувачів
            var users = new List<object>
            {
                new { Id = 1, Name = "Іван", Email = "ivan@answear.com", Role = "User" },
                new { Id = 2, Name = "Анна", Email = "anna@answear.com", Role = "Admin" }
            };
            return Ok(users);
        }

        // DELETE: api/admin/user/1
        [HttpDelete("user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(new { Message = $"Користувача з ID {id} було видалено." });
        }

        // POST: api/admin/user
        [HttpPost("user")]
        public IActionResult AddUser([FromBody] UserDto user)
        {
            return Ok(new { Message = $"Користувача {user.Name} додано." });
        }

        // PUT: api/admin/user/1
        [HttpPut("user/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto user)
        {
            return Ok(new { Message = $"Користувача з ID {id} оновлено." });
        }

        
        public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
