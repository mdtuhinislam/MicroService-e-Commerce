using Ordering.Application.Contract.Infrastructures;
using Ordering.Application.Models;
using QuickMailer;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
            
        }
        public  Task<bool> SendEmailAsync(EmailMassage emailMassage)
        {
            Email email = new Email();
            if (email.IsValidEmail(emailMassage.To))
            {
                return Task.FromResult<bool>(email.SendEmail(emailMassage.To, "tuhin.testdev@gmail.com", "bzsxfocqrkkhtqul", emailMassage.Subject, emailMassage.Body));
            }
            return Task.FromResult<bool>(false);
            
            
        }
    }
}
