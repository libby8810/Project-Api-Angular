using System.Threading.Tasks;

namespace StoreApi.Services
{
    public interface IEmailService
    {
        Task<bool> SendGiftWonEmailAsync(string toEmail, string giftName);
    }
}