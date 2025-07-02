using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IJwtService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
} 