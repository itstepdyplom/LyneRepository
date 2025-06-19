using System.ComponentModel.DataAnnotations;
using Lyne.Domain.Entities;

namespace Lyne.Application.DTO;

public class CategoryDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required!"),MaxLength(50)]
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<Guid> ProductIds { get; set; } = new();
}
