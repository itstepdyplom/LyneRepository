using Lyne.Application.DTO;
using Lyne.Application.DTO.Auth;

namespace Lyne.Application.Services;

public interface IEmailSender
{
    public Task<bool> RegisterNotification(RegisterRequestDto userDto);
    public Task UpdatesNotification(LoginUserDto userDto);
}