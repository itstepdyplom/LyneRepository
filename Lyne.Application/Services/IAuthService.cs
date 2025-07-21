using Lyne.Application.DTO.Auth;

namespace Lyne.Application.Services;

public interface IAuthService
{
    public Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    public Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);
    public Task<AuthResponseDto?> LoginWithGoogleAsync(string email, string? fullName);
}