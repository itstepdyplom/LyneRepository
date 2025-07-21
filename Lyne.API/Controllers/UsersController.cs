using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class UsersController(IUserService userService,ILogger<UsersController> logger) : BaseController
    {
        [HttpGet("/users")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            logger.LogInformation("Отримано запит на отримання всіх користувачів");
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("/user/{id}")]
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

        [HttpPost("/user/create")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Post([FromBody] UserDto dto)
        {
            logger.LogInformation("Запит на створення користувача");

            try
            {
                var success = await userService.AddAsync(dto);
                if (!success)
                {
                    logger.LogWarning("Не вдалося створити користувача. DTO: {@Dto}", dto);
                    return BadRequest("Не вдалось створити користувача");
                }

                logger.LogInformation("Користувача з ID = {Id} успішно створено", dto.Id);
                return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні користувача");
                return StatusCode(500, "Внутрішня помилка сервера");
            }
        }

        [HttpPut("/user/update")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Put([FromBody] UserDto dto)
        {
            logger.LogInformation("Запит на оновлення користувача з ID = {Id}", dto.Id);
            var success = await userService.UpdateAsync(dto);
            if (!success)
            {
                logger.LogWarning("Користувача з ID = {Id} не знайдено для оновлення", dto.Id);
                return NotFound();
            }

            logger.LogInformation("Користувача з ID = {Id} успішно оновлено", dto.Id);
            return NoContent();
        }

        [HttpDelete("/user/delete/{id}")]
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

        [HttpPost("/createUserWithAddress")]
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
        [HttpPost("/updateUserWithAddress")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<bool>> UpdateUserWithAddress(UserDto dto)
        {
            logger.LogInformation("Запит на оновлення користувача з ID = {Id} з адресою", dto.Id);
            try
            {
                var result = await userService.UpdateAsync(dto);
                logger.LogInformation("Користувача з ID = {Id} успішно оновлено з адресою", dto.Id);
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
