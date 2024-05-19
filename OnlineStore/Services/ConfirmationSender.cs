using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Contracts.Services;

namespace Services
{
    public class ConfirmationSender : IConfirmationSender
    {
        private readonly IEmailSender _emailSender;

        public ConfirmationSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task SendEmailConfirmationAsync(string email, string link)
        {
            return _emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public Task SendEmailConfirmationAsync(string email, Uri link)
        {
            return _emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href=\"" + link + "\">link</a>");
        }
    }
}
