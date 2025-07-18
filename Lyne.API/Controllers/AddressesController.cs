using Lyne.Application.DTO;
using Lyne.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lyne.API.Controllers
{
    public class AddressesController(IAddressService addressService, ILogger<AddressService> logger) : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> Get(int id)
        {
            logger.LogInformation("Запит на отримання адреси з ID = {Id}", id);
            var address = await addressService.GetByIdAsync(id);
            if (address == null)
            {
                logger.LogWarning("Адреса з ID = {Id} не знайдена", id);
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddressDto dto)
        {
           logger.LogInformation("Запит на створення адреси");

            try
            {
                var success = await addressService.AddAsync(dto);
                if (!success)
                {
                    logger.LogWarning("Не вдалося створити адресу. DTO: {@Dto}", dto);
                    return BadRequest("Не вдалося створити адресу.");
                }

                logger.LogInformation("Адреса успішно створена");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Помилка при створенні адреси");
                return StatusCode(500, "Внутрішня помилка сервера");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AddressDto dto)
        {
            logger.LogInformation("Запит на оновлення адреси з ID = {Id}", dto.Id);
            var success = await addressService.UpdateAsync(dto);
            if (!success)
            {
                logger.LogWarning("Адреса з ID = {Id} не знайдена для оновлення", dto.Id);
                return NotFound();
            }

            logger.LogInformation("Адреса з ID = {Id} успішно оновлена", dto.Id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] AddressDto dto)
        {
           logger.LogInformation("Запит на видалення адреси з ID = {Id}", dto.Id);
            var success = await addressService.DeleteAsync(dto.Id);
            if (!success)
            {
                logger.LogWarning("Адреса з ID = {Id} не знайдена для видалення", dto.Id);
                return NotFound();
            }

            logger.LogInformation("Адреса з ID = {Id} успішно видалена", dto.Id);
            return NoContent();
        }

    }
}