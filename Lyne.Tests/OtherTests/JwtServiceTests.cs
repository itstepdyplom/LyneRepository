using FluentAssertions;
using Lyne.Domain.Entities;
using Lyne.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Claims; // Add this

namespace Lyne.Tests.OtherTests;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;

    public JwtServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:SecretKey", "0123456789abcdef0123456789abcdef"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _jwtService = new JwtService(configuration);
    }

    [Fact]
    public void GenerateToken_ReturnsValidToken()
    {
        var user = new User { Id = 1, Email = "test@example.com", Name = "Test", ForName = "test", PasswordHash = "test" };
        var token = _jwtService.GenerateToken(user);
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ValidateToken_ReturnsClaimsPrincipal_WhenTokenIsValid()
    {
        var user = new User { Id = 1, Email = "test@example.com", Name = "Test", ForName = "test", PasswordHash = "test"};
        var token = _jwtService.GenerateToken(user);
        ClaimsPrincipal? principal = _jwtService.ValidateToken(token);
        principal.Should().NotBeNull();
        principal.Identity.Should().NotBeNull();
        principal.Identity.IsAuthenticated.Should().BeTrue();
    }

    [Fact]
    public void ValidateToken_ReturnsNull_WhenTokenIsInvalid()
    {
        var principal = _jwtService.ValidateToken("invalid.token.value");
        principal.Should().BeNull();
    }
}
