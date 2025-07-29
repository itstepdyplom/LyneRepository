using System.Security.Claims;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal ValidateToken(string token);
} 