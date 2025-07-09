using Lyne.Application.DTO;
using Lyne.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            logger.LogInformation("Запит на отримання всіх категорій");
            var categories = await categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(Guid id)
        {
            logger.LogInformation("Запит на отримання категорії з ID = {Id}", id);
            var category = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                logger.LogWarning("Категорія з ID = {Id} не знайдена", id);
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto dto)
        {
            logger.LogInformation("Запит на створення категорії");

            try
            {
                var success = await categoryService.AddAsync(dto);
                if (!success)
                {
                    logger.LogWarning("Не вдалося створити категорію. DTO: {@Dto}", dto);
                    return BadRequest("Не вдалося створити категорію.");
                }

                logger.LogInformation("Категорія успішно створена з ID = {Id}", dto.Id);
                return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні категорії");
                return StatusCode(500, "Внутрішня помилка сервера");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CategoryDto dto)
        {
            if (id != dto.Id)
            {
                logger.LogWarning("Id у URL ({UrlId}) не збігається з Id у тілі запиту ({BodyId})", id, dto.Id);
                return BadRequest("Id в маршруті не співпадає з Id у тілі запиту.");
            }

            logger.LogInformation("Запит на оновлення категорії з ID = {Id}", id);
            var success = await categoryService.UpdateAsync(dto);
            if (!success)
            {
                logger.LogWarning("Категорія з ID = {Id} не знайдена для оновлення", id);
                return NotFound();
            }

            logger.LogInformation("Категорія з ID = {Id} успішно оновлена", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
           logger.LogInformation("Запит на видалення категорії з ID = {Id}", id);
            var success = await categoryService.DeleteAsync(id);
            if (!success)
            {
                logger.LogWarning("Категорія з ID = {Id} не знайдена для видалення", id);
                return NotFound();
            }

            logger.LogInformation("Категорія з ID = {Id} успішно видалена", id);
            return NoContent();
        }
    }
}
