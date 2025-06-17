namespace Lyne.Domain.Entities;

public class Order
{
    public int Id { get; set; } // ← це має бути!
    public DateTime Date { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
    public ICollection<Product> Products { get; set; }
}
