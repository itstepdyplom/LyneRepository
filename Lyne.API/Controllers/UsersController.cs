using Lyne.Application.DTO;
using Lyne.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class UsersController(IUserService userService,ILogger<UsersController> logger) : BaseController
    {
        [HttpGet("/users")]
        public async Task<List<UserDto>> Get()
        {
            return await userService.GetAllAsync();
        }

        [HttpGet("/user/{id}")]
        public async Task<UserDto?> Get(int id)
        {
            return await userService.GetByIdAsync(id);
        }

        [HttpPost("/user/create")]
        public void Post([FromBody] UserDto dto)
        {
            userService.AddAsync(dto);
        }

        [HttpPut("/user/update")]
        public void Put([FromBody] UserDto dto)
        {
            userService.UpdateAsync(dto);
        }

        [HttpDelete("/user/delete/{id}")]
        public void Delete(int id)
        {
            userService.DeleteAsync(id);
        }

        [HttpPost("/addUserWithAddress")]
        public async Task<bool> AddUserWithAddress([FromBody]UserDto dto)
        {
            return await userService.AddAsync(dto);
        }
        [HttpPost("/updateUserWithAddress")]
        public async Task<bool> UpdateUserWithAddress(UserDto dto)
        {
            return await userService.UpdateAsync(dto);
        }
    }
}
