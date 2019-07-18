using PayPal.v1.Payments;
using System.Threading.Tasks;

namespace EMarket.Services.PayPal
{
    public interface IPayPalPayment
    {
        Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent);

        Task<Payment> ExecutePayment(Payment payment);

    }
}
