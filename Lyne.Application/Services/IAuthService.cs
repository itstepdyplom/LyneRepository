using Lyne.Application.DTO.Auth;
using Lyne.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Lyne.Application.Services;

public interface IAuthService
{
    public Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    public Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);
    public Task<AuthResponseDto?> LoginWithGoogleAsync(string email, string? fullName);
    public Task<User?> GetCurrentUserAsync(HttpContext context);
}