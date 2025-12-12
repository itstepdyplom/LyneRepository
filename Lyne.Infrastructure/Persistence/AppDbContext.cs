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
        public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();
            
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .UseIdentityColumn();
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);
            
            modelBuilder.Entity<OrderProduct>(e =>
            {
                e.ToTable("order_products");

                e.HasKey(x => new { x.OrderId, x.ProductId });

                e.Property(x => x.OrderId).HasColumnName("order_id");
                e.Property(x => x.ProductId).HasColumnName("product_id");
                e.Property(x => x.Quantity).HasColumnName("quantity");
                e.Property(x => x.UnitPrice).HasColumnName("unit_price");

                e.HasOne(x => x.Order)
                    .WithMany(o => o.OrderProducts)
                    .HasForeignKey(x => x.OrderId);

                e.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId);
            });
            
            // modelBuilder.Entity<OrderProduct>()
            //     .HasKey(x => new { x.OrderId, x.ProductId });
            //
            // modelBuilder.Entity<OrderProduct>()
            //     .HasOne(x => x.Order)
            //     .WithMany(o => o.OrderProducts)
            //     .HasForeignKey(x => x.OrderId);
            //
            // modelBuilder.Entity<OrderProduct>()
            //     .HasOne(x => x.Product)
            //     .WithMany() // або .WithMany(p => p.OrderProducts)
            //     .HasForeignKey(x => x.ProductId);
            
            // modelBuilder.Entity<Product>().ToTable("products");
            // modelBuilder.Entity<Category>().ToTable("categories");
            // modelBuilder.Entity<User>().ToTable("users");
            // modelBuilder.Entity<Address>().ToTable("addresses");
            // modelBuilder.Entity<Order>().ToTable("orders");
            
            //modelBuilder.Entity<Product>().Ignore(e => e.RequestClientOptions);
            // modelBuilder.Entity<Product>().Ignore("BaseUrl");
            // modelBuilder.Entity<Product>().Ignore("PrimaryKey");

 modelBuilder.Entity<User>(e =>
    {
        e.ToTable("users");                 // таблиця в нижньому регістрі
        e.Property(x => x.Id).HasColumnName("id");
        e.Property(x => x.Name).HasColumnName("name");
        e.Property(x => x.ForName).HasColumnName("for_name");
        e.Property(x => x.Gender).HasColumnName("gender");
        e.Property(x => x.PasswordHash).HasColumnName("password_hash");
        e.Property(x => x.PhoneNumber).HasColumnName("phone_number");
        e.Property(x => x.Email).HasColumnName("email");
        e.Property(x => x.AddressId).HasColumnName("address_id");
        e.Property(x => x.Role).HasColumnName("role");
        e.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamptz");
        e.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamptz");
        e.Property(x => x.DateOfBirth).HasColumnName("date_of_birth").HasColumnType("date");
    });

    modelBuilder.Entity<Address>(e =>
    {
        e.ToTable("addresses");
        e.Property(x => x.Id).HasColumnName("id");
        e.Property(x => x.Street).HasColumnName("street");
        e.Property(x => x.City).HasColumnName("city");
        e.Property(x => x.State).HasColumnName("state");
        e.Property(x => x.Zip).HasColumnName("zip");
        e.Property(x => x.Country).HasColumnName("country");
    });
    modelBuilder.Entity<Address>()
        .HasKey(a => a.Id);

    modelBuilder.Entity<Address>()
        .Property(a => a.Id)
        .UseIdentityByDefaultColumn();

    modelBuilder.Entity<Category>(e =>
    {
        e.ToTable("categories");
        e.Property(x => x.Id).HasColumnName("id");
        e.Property(x => x.Name).HasColumnName("name");
        e.Property(x => x.Description).HasColumnName("description");
    });

    modelBuilder.Entity<Product>(e =>
    {
        e.ToTable("products");
        e.Property(x => x.Id).HasColumnName("id");
        e.Property(x => x.Name).HasColumnName("name");
        e.Property(x => x.Brand).HasColumnName("brand");
        e.Property(x => x.Price).HasColumnName("price");
        e.Property(x => x.CategoryId).HasColumnName("category_id");
        e.Property(x => x.Description).HasColumnName("description");
        e.Property(x => x.StockQuantity).HasColumnName("stock_quantity");
        e.Property(x => x.ImageUrl).HasColumnName("image_url");
        e.Property(x => x.Size).HasColumnName("size");
        e.Property(x => x.Color).HasColumnName("color");
        e.Property(x => x.IsActive).HasColumnName("is_active");
        e.Property(x => x.CreatedAt).HasColumnName("created_at");
        e.Property(x => x.UpdatedAt).HasColumnName("updated_at");
    });

    modelBuilder.Entity<Order>(e =>
    {
        e.ToTable("orders");
        e.Property(x => x.Id).HasColumnName("id");
        e.Property(x => x.UserId).HasColumnName("user_id");
        e.Property(x => x.ShippingAddressId).HasColumnName("shipping_address_id");
        e.Property(x => x.PaymentMethod).HasColumnName("payment_method");
        e.Property(x => x.TrackingNumber).HasColumnName("tracking_number");
        
        e.Property(x => x.OrderStatus)
            .HasConversion<string>()
            .HasColumnName("order_status");
        
        e.Property(x => x.Date).HasColumnName("date").HasColumnType("timestamptz");
        e.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamptz");
        e.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamptz");
    });

    // many-to-many -> таблиця order_products з snake_case
    // modelBuilder.Entity<Order>()
    //     .HasMany(o => o.Products)
    //     .WithMany()
    //     .UsingEntity<Dictionary<string, object>>(
    //         "order_products",
    //         r => r.HasOne<Product>().WithMany().HasForeignKey("product_id").OnDelete(DeleteBehavior.Restrict),
    //         l => l.HasOne<Order>().WithMany().HasForeignKey("order_id").OnDelete(DeleteBehavior.Cascade));

            // modelBuilder.Entity<Address>()
            //     .HasOne(a => a.User)
            //     .WithMany()
            //     .HasForeignKey(a => a.UserId);

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
                    Gender = "Жіноча",
                    PasswordHash = "hashedpassword123",
                    DateOfBirth = new DateOnly(2002, 3, 15),
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
                    Gender = "Чоловіча",
                    PasswordHash = "hashedpassword123",
                    DateOfBirth = new DateOnly(2000, 6, 18),
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