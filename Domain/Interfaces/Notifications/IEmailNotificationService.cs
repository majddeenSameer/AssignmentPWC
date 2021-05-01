using System.Net.Mail;
using System.Threading.Tasks;

namespace Tahaluf.FCA.CashDeclaration.Domain.Interfaces.Notification
{
    public interface IEmailNotificationService
    {
        Task SendEmailNotificationAsync(string emailTo, string emailSubject, string emailBoady, Attachment attachment = null);
    }
}
