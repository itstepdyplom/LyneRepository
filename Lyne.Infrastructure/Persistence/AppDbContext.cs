using Lyne.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lyne.Infrastructure.Persistence;

public class AppDbContext:DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Seed Address
        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = 1,
                Street = "вул. Січових Стрільців, 12",
                City = "Львів",
                State = "Львівська",
                Zip = "79000",
                Country = "Україна"
            },
            new Address
            {
                Id = 2,
                Street = "вул. Тараса Шевченка, 115",
                City = "Київ",
                State = "Київська",
                Zip = "01001",
                Country = "Україна"
            }
        );

        //Seed Users
        modelBuilder.Entity<User>().HasData(
           new User
           {
               Id = 1,
               Name = "Ольга",
               ForName = "Косач",
               Genre = "Жіноча",
               DateOfBirth = new DateTime(2002, 3, 15),
               PhoneNumber = "+380501234567",
               Email = "kosacho@gmail.com",
               AddressId = 1,
               CreatedAt = new DateTime(2024, 6, 1),
               UpdatedAt = new DateTime(2024, 6, 1)
           },
            new User
            {
                Id = 2,
                Name = "Алекс",
                ForName = "Кочмар",
                Genre = "Чоловіча",
                DateOfBirth = new DateTime(2000, 6, 18),
                PhoneNumber = "+380986199887",
                Email = "alekskochmar18@gmail.com",
                AddressId = 2,
                CreatedAt = new DateTime(2025, 2, 15),
                UpdatedAt = new DateTime(2025, 2, 15)
            }
        );

        //Seed Categories
        var menCategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var womenCategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = menCategoryId,
                Name = "Чоловічий одяг",
                Description = "Куртки, футболки, штани та інший одяг для чоловіків"
            },
            new Category
            {
                Id = womenCategoryId,
                Name = "Жіночий одяг",
                Description = "Сукні, спідниці, топи, костюми для жінок"
            }
        );

        //Seed Products
        var product1Id = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var product2Id = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = product1Id,
                Name = "Футболка чоловіча BASIC",
                Brand = "Zara",
                Price = 599.00m,
                CategoryId = menCategoryId,
                Description = "Класична футболка з бавовни, біла",
                StockQuantity = 100,
                ImageUrl = "https://example.com/images/mens-tshirt.jpg",
                Size = "L",
                Color = "Білий",
                IsActive = true,
                CreatedAt = new DateTime(2024, 6, 1),
                UpdatedAt = new DateTime(2024, 6, 1)
            },
            new Product
            {
                Id = product2Id,
                Name = "Сукня вечірня ELEGANT",
                Brand = "Mango",
                Price = 1599.00m,
                CategoryId = womenCategoryId,
                Description = "Вечірня сукня з відкритими плечима, синя",
                StockQuantity = 50,
                ImageUrl = "https://example.com/images/womens-dress.jpg",
                Size = "M",
                Color = "Синій",
                IsActive = true,
                CreatedAt = new DateTime(2024, 6, 1),
                UpdatedAt = new DateTime(2024, 6, 1)
            }
        );
    }
}
