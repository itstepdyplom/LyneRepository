using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            logger.LogInformation("Запит на отримання всіх замовлень");
            var orders = await orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            logger.LogInformation("Запит на отримання замовлення з ID = {Id}", id);
            var order = await orderService.GetByIdAsync(id);
            if (order == null)
            {
                logger.LogWarning("Замовлення з ID = {Id} не знайдено", id);
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] OrderDto dto)
        {
            logger.LogInformation("Запит на створення нового замовлення");
            try
            {
                var result = await orderService.AddAsync(dto);
                if (!result)
                {
                    logger.LogWarning("Не вдалося створити замовлення: валідація не пройдена");
                    return BadRequest("Не вдалося створити замовлення.");
                }

                logger.LogInformation("Замовлення успішно створено");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні замовлення");
                return StatusCode(500, "Внутрішня помилка сервера");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Manager))]
        public async Task<ActionResult> Put(int id, [FromBody] OrderDto dto)
        {
            if (id != dto.Id)
            {
                logger.LogWarning("ID в URL ({UrlId}) не збігається з ID в тілі запиту ({DtoId})", id, dto.Id);
                return BadRequest("ID в URL і DTO не збігаються.");
            }

            logger.LogInformation("Запит на оновлення замовлення з ID = {Id}", id);

            var result = await orderService.UpdateAsync(dto);
            if (!result)
            {
                logger.LogWarning("Не вдалося оновити замовлення з ID = {Id}", id);
                return NotFound();
            }

            logger.LogInformation("Замовлення з ID = {Id} успішно оновлено", id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogInformation("Запит на видалення замовлення з ID = {Id}", id);

            var result = await orderService.DeleteAsync(id);
            if (!result)
            {
                logger.LogWarning("Замовлення з ID = {Id} не знайдено для видалення", id);
                return NotFound();
            }

            logger.LogInformation("Замовлення з ID = {Id} успішно видалено", id);
            return Ok();
        }
    }
}