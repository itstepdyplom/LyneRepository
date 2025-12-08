using Lyne.Application.DTO;
using Lyne.Application.DTO.Auth;
using Lyne.Application.Services;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Lyne.Infrastructure.Services;

public class EmailSender(IConfiguration configuration):IEmailSender
{
    public async Task<bool> RegisterNotification(RegisterRequestDto userDto)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Lyne","lyneconceptdev@gmail.com"));
        message.To.Add(new MailboxAddress(userDto.Name+userDto.ForName ?? "",userDto.Email));
        message.Subject = "Welcome to Lyne ✨";
        var html = $@"
        <div style=""font-family: Arial, sans-serif; padding: 20px; background-color: #f6f6f6;"">
            <div style=""max-width: 600px; margin: 0 auto; background: white; padding: 25px; border-radius: 10px;"">

                <h2 style=""color: #333; text-align:center; margin-bottom: 20px;"">
                    Welcome to <span style=""color:#6a5acd"">Lyne</span> 👗✨
                </h2>

                <p style=""font-size: 16px; color: #555;"">
                    Hi <strong>{userDto.Name} {userDto.ForName}</strong>,<br/><br/>
                    Thank you for registering at <strong>Lyne</strong>.  
                    We're excited to have you with us!
                </p>

                <p style=""font-size: 16px; color: #555;"">
                    From now on, you'll receive updates about new arrivals, special offers,  
                    and exclusive promotions available only to our community.
                </p>

                <div style=""text-align:center; margin: 30px 0;"">
                    <a href=""https://lyne-shop.com"" 
                       style=""padding: 12px 22px; background:#6a5acd; color:white;
                              text-decoration:none; border-radius:6px; font-size:16px;"">
                        Visit Store
                    </a>
                </div>

                <hr style=""border:none; height:1px; background:#ddd; margin:25px 0;""/>

                <p style=""font-size: 14px; color:#777; text-align:center;"">
                    © {DateTime.UtcNow.Year} Lyne — Fashion that defines you.
                </p>
            </div>
        </div>";
        var builder = new BodyBuilder
        {
            HtmlBody = html
        };

        message.Body = builder.ToMessageBody();

        var section = configuration.GetSection("EmailSender");
        var email = section["Email"];
        var password = section["Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(email, password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task UpdatesNotification(LoginUserDto userDto)
    {
        throw new NotImplementedException();
    }
}