using Lyne.Domain.Entities;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace Lyne.Tests.RepositoriesTests
{
    public class AuthRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly AuthRepository _repository;

        public AuthRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("DataSource=:memory:")
                .Options;
            _context = new AppDbContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _repository = new AuthRepository(_context);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddUser()
        {
            var user = new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                ForName = "Tester",
                Gender = "M",
                PhoneNumber = "1234567890",
                DateOfBirth = System.DateTime.UtcNow,
                PasswordHash = "hashedpassword",
                Role = "User"
            };

            var createdUser = await _repository.CreateUserAsync(user);

            Assert.NotNull(createdUser);
            Assert.Equal("testuser@example.com", createdUser.Email);

            var dbUser = await _context.Users.FindAsync(createdUser.Id);
            Assert.NotNull(dbUser);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            var user = new User
            {
                Name = "Find Me",
                Email = "findme@example.com",
                ForName = "Finder",
                Gender = "F",
                PhoneNumber = "0987654321",
                DateOfBirth = System.DateTime.UtcNow,
                PasswordHash = "hash",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var foundUser = await _repository.GetUserByEmailAsync("findme@example.com");

            Assert.NotNull(foundUser);
            Assert.Equal(user.Email, foundUser.Email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var result = await _repository.GetUserByEmailAsync("nonexistent@example.com");
            Assert.Null(result);
        }

        [Fact]
        public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
        {
            var user = new User
            {
                Name = "Exists",
                Email = "exists@example.com",
                ForName = "Existor",
                Gender = "M",
                PhoneNumber = "1111111111",
                DateOfBirth = System.DateTime.UtcNow,
                PasswordHash = "hash",
                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var exists = await _repository.UserExistsAsync("exists@example.com");
            Assert.True(exists);
        }

        [Fact]
        public async Task UserExistsAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var exists = await _repository.UserExistsAsync("unknown@example.com");
            Assert.False(exists);
        }

        [Fact]
        public async Task CreateAddressAsync_ShouldAddAddress()
        {
            var address = new Address
            {
                City = "Test City",
                Country = "Test Country",
                State = "Test State",
                Street = "123 Test St",
                Zip = "12345",
            };

            var createdAddress = await _repository.CreateAddressAsync(address);

            Assert.NotNull(createdAddress);
            Assert.Equal("Test City", createdAddress.City);

            var dbAddress = await _context.Addresses.FindAsync(createdAddress.Id);
            Assert.NotNull(dbAddress);
        }
    }
}
