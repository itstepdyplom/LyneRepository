using System.Security.Claims;
using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal ValidateToken(string token);
} 