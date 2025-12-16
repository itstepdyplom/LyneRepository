using FluentAssertions;
using Lyne.Application.DTO;

namespace Lyne.Tests.DtosEntitiesTests;

public class RegisterUserDtoTests
{
    [Fact]
    public void Properties_SetAndGet_Works()
    {
        var dto = new RegisterUserDto
        {
            Name = "Test",
            ForName = "User",
            Email = "test@example.com",
            Password = "pass",
            DateOfBirth = new DateOnly()
        };
        dto.Name.Should().Be("Test");
        dto.ForName.Should().Be("User");
        dto.Email.Should().Be("test@example.com");
        dto.Password.Should().Be("pass");
    }
}
