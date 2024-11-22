using Ordering.Application.Models;

namespace Ordering.Application.Contract.Infrastructures
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
