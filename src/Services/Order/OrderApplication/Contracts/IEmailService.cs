using System.Threading.Tasks;
using OrderApplication.Models;

namespace OrderApplication.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}