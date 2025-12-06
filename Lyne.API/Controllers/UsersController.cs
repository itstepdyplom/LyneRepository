using Lyne.Application.DTO;
using Lyne.Application.DTO.Auth;
using Lyne.Application.Services;
using Lyne.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class UsersController(IUserService userService,ILogger<UsersController> logger) : BaseController
    {
        [HttpGet("/api/Users")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            logger.LogInformation("Отримано запит на отримання всіх користувачів");
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("/api/User/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<UserDto?>> Get(int id)
        {
            logger.LogInformation("Отримано запит на отримання користувача з ID = {Id}", id);
            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                logger.LogWarning("Користувача з ID = {Id} не знайдено", id);
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("/api/User")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Post([FromBody] UserDto dto)
        {
            logger.LogInformation("Request for creation user");

            try
            {
                var success = await userService.AddAsync(dto);
                // if (!success)
                // {
                //     logger.LogWarning("Не вдалося створити користувача. DTO: {@Dto}", dto);
                //     return BadRequest("Не вдалось створити користувача");
                // }

                logger.LogInformation("Користувача з ID = {Id} успішно створено", dto.Id);
                return StatusCode(200,$"user with name: {dto.Name} created");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні користувача");
                return StatusCode(500, "Server error");
            }
        }

        [HttpPut("/api/User/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Put(int id, [FromBody] UserUpdateDto dto)
        {
            logger.LogInformation("Запит на оновлення користувача  {name} {forname}", dto.Name, dto.ForName);
            var success = await userService.UpdateAsync(id,dto);
            if (!success)
            {
                logger.LogWarning("Користувача не знайдено для оновлення");
                return NotFound();
            }

            logger.LogInformation("Користувача успішно оновлено");
            return NoContent();
        }

        [HttpDelete("/api/User/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogInformation("Запит на видалення користувача з ID = {Id}", id);
            var success = await userService.DeleteAsync(id);
            if (!success)
            {
                logger.LogWarning("Користувача з ID = {Id} не знайдено для видалення", id);
                return NotFound();
            }

            logger.LogInformation("Користувача з ID = {Id} успішно видалено", id);
            return NoContent();
        }

        [HttpPost("/CreateUserWithAddress")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<bool>> AddUserWithAddress([FromBody] UserDto dto)
        {
            logger.LogInformation("Запит на створення користувача з адресою");
            try
            {
                var result = await userService.AddAsync(dto);
                logger.LogInformation("Користувача з ID = {Id} успішно створено з адресою", dto.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні користувача з адресою");
                return StatusCode(500, false);
            }
        }
        [HttpPost("/UpdateUserWithAddress")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<bool>> UpdateUserWithAddress(int id,UserUpdateDto dto)
        {
            logger.LogInformation("Запит на оновлення користувача {name} {forname} з адресою", dto.Name,dto.ForName);
            try
            {
                var result = await userService.UpdateAsync(id,dto);
                logger.LogInformation("Користувача успішно оновлено з адресою");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при оновленні користувача з адресою");
                return StatusCode(500, false);
            }
        }
    }
}
