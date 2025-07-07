using FluentAssertions;
using Lyne.Application.DTO;

namespace Lyne.Tests.DtosEntitiesTests;

public class LoginUserDtoTests
{
    [Fact]
    public void Properties_SetAndGet_Works()
    {
        var dto = new LoginUserDto
        {
            Email = "test@example.com",
            Password = "password"
        };
        dto.Email.Should().Be("test@example.com");
        dto.Password.Should().Be("password");
    }
}
