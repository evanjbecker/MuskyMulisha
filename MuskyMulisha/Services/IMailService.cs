using System.Threading.Tasks;
using MuskyMulisha.Models;

namespace MuskyMulisha.Services
{
    public interface IMailService
    {
        Task<EmailStatus> SendAsync(EmailModel emailModel);
    }
}