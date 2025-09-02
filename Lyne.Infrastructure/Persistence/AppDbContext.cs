using Lyne.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lyne.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>().ToTable("Address");

        /*modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);*/

        //Seed Address
        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = 1,
                Street = "вул. Січових Стрільців, 12",
                City = "Львів",
                State = "Львівська",
                Zip = "79000",
                Country = "Україна",
            },
            new Address
            {
                Id = 2,
                Street = "вул. Тараса Шевченка, 115",
                City = "Київ",
                State = "Київська",
                Zip = "01001",
                Country = "Україна",
            },
            new Address
            {
                Id = 3,
                Street = "вул. Хрещатик, 1",
                City = "Київ",
                State = "Київська",
                Zip = "01001",
                Country = "Україна",
            },
            new Address
            {
                Id = 4,
                Street = "вул. Дерибасівська, 10",
                City = "Одеса",
                State = "Одеська",
                Zip = "65000",
                Country = "Україна",
            },
            new Address
            {
                Id = 5,
                Street = "Default Address",
                City = "Default City",
                State = "Default State",
                Zip = "00000",
                Country = "Україна",
            },
            new Address
            {
                Id = 6,
                Street = "вул. Соборна, 25",
                City = "Дніпро",
                State = "Дніпропетровська",
                Zip = "49000",
                Country = "Україна",
            },
            new Address
            {
                Id = 7,
                Street = "вул. Сумська, 50",
                City = "Харків",
                State = "Харківська",
                Zip = "61000",
                Country = "Україна",
            },
            new Address
            {
                Id = 8,
                Street = "вул. Героїв Майдану, 33",
                City = "Запоріжжя",
                State = "Запорізька",
                Zip = "69000",
                Country = "Україна",
            },
            new Address
            {
                Id = 9,
                Street = "вул. Центральна, 15",
                City = "Полтава",
                State = "Полтавська",
                Zip = "36000",
                Country = "Україна",
            },
            new Address
            {
                Id = 10,
                Street = "вул. Миру, 8",
                City = "Чернівці",
                State = "Чернівецька",
                Zip = "58000",
                Country = "Україна",
            }
        );

        //Seed Users with static password hashes (password: password123)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Ольга",
                ForName = "Косач",
                Genre = "Жіноча",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2002, 3, 15),
                PhoneNumber = "+380501234567",
                Email = "kosacho@gmail.com",
                CreatedAt = new DateTime(2024, 6, 1),
                UpdatedAt = new DateTime(2024, 6, 1),
                Role = "User",
                AddressId = 1
            },
            new User
            {
                Id = 2,
                Name = "Алекс",
                ForName = "Кочмар",
                Genre = "Чоловіча",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 6, 18),
                PhoneNumber = "+380986199887",
                Email = "alekskochmar18@gmail.com",
                CreatedAt = new DateTime(2024, 2, 15),
                UpdatedAt = new DateTime(2024, 2, 15),
                Role = "User",
                AddressId = 2
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