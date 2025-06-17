namespace Lyne.Domain.Entities;

public class User
{
    public int Id { get; set; }   // ← ключ
    public string Name { get; set; }
    public string Email { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}
