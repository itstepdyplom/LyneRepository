namespace Lyne.Application.DTO.Auth;

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string ForName { get; set; }
    public DateTime ExpiresAt { get; set; }
} 