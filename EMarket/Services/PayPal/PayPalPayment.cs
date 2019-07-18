using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraintreeHttp;
using Microsoft.Extensions.Options;
using PayPal.Core;
using PayPal.v1.PaymentExperience;
using PayPal.v1.Payments;

namespace EMarket.Services.PayPal
{
    public class PayPalPayment : IPayPalPayment
    {   
        
        private readonly PayPalAuthOptions _option;
        private SandboxEnvironment _sandbox;
        private PayPalEnvironment _paypalEnvironment;
        PayPalHttpClient _client;

        public PayPalPayment(IOptions<PayPalAuthOptions> option)
        {
            
            _option = option.Value;
            _sandbox = new SandboxEnvironment(_option.PayPalClientID, _option.PayPalSecret);
            _client = new PayPalHttpClient(_sandbox);
        }

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent)
        {
            var payment = new Payment()
            {
                Intent = intent,
                Transactions = GetTransactionsList(amount),
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = cancelUrl,
                    ReturnUrl = returnUrl
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            
            return payment;
        }


        private List<Transaction> GetTransactionsList(decimal amount)
        {
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                Description = "Transaction description.",
                InvoiceNumber = GetRandomInvoiceNumber(),
                Amount = new Amount()
                {
                    Currency = "USD",
                    Total = amount.ToString(),
                    Details = new AmountDetails()
                    {
                        Tax = "0",
                        Shipping = "0",
                        Subtotal = amount.ToString()
                    }
                },
                ItemList = new ItemList()
                {
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Payment",
                            Currency = "USD",
                            Price = amount.ToString(),
                            Quantity = "1",
                            Sku = "sku"
                        }
                    }
                },
                Payee = new Payee
                {
                    // TODO.. Enter the payee email address here
                    Email = "",

                    // TODO.. Enter the merchant id here
                    MerchantId = ""
                }
            });

            return transactionList;
        }

        public async Task<Payment> ExecutePayment(Payment payment)
        {
            Payment result = new Payment();
            try
            {
                PaymentCreateRequest request = new PaymentCreateRequest();
                request.RequestBody(payment);           
                HttpResponse response = await _client.Execute(request);
                var statusCode = response.StatusCode;
                result = response.Result<Payment>();
                return result;
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
            }
            return result;
        }

        private string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999999).ToString();
        }
    }
}
