using System.Threading.Tasks;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class ProductsController(IProductService productService, ILogger<ProductsController> logger) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            logger.LogInformation("Запит на отримання всіх продуктів");
            var products = await productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(Guid id)
        {
           logger.LogInformation("Запит на отримання продукту з ID = {Id}", id);
            var product = await productService.GetByIdAsync(id);
            if (product == null)
            {
                logger.LogWarning("Продукт з ID = {Id} не знайдено", id);
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto dto)
        {
            logger.LogInformation("Запит на створення продукту");

            try
            {
                var success = await productService.AddAsync(dto);
                if (!success)
                {
                    logger.LogWarning("Не вдалося створити продукт. DTO: {@Dto}", dto);
                    return BadRequest("Не вдалося створити продукт.");
                }

                logger.LogInformation("Продукт успішно створено з ID = {Id}", dto.Id);
                return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні продукту");
                return StatusCode(500, "Внутрішня помилка сервера");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id)
            {
                logger.LogWarning("Id у URL ({UrlId}) не збігається з Id у тілі запиту ({BodyId})", id, dto.Id);
                return BadRequest("Id в маршруті не співпадає з Id у тілі запиту.");
            }

            logger.LogInformation("Запит на оновлення продукту з ID = {Id}", id);
            var success = await productService.UpdateAsync(dto);
            if (!success)
            {
                logger.LogWarning("Продукт з ID = {Id} не знайдено для оновлення", id);
                return NotFound();
            }

            logger.LogInformation("Продукт з ID = {Id} успішно оновлено", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            logger.LogInformation("Запит на видалення продукту з ID = {Id}", id);
            var success = await productService.DeleteAsync(id);
            if (!success)
            {
                logger.LogWarning("Продукт з ID = {Id} не знайдено для видалення", id);
                return NotFound();
            }

            logger.LogInformation("Продукт з ID = {Id} успішно видалено", id);
            return NoContent();
        }
    }
}
