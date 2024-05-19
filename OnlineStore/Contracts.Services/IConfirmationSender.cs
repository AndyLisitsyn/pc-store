using System;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IConfirmationSender
    {
        Task SendEmailConfirmationAsync(string email, string link);
        Task SendEmailConfirmationAsync(string email, Uri link);
    }
}
