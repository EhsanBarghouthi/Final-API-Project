using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RDP_NTier_Task.BL.General_Services;
using RDP_NTier_Task.BL.ServicesRepository.CartServices;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.CartRepository;
using RDP_NTier_Task.DAL.Repostry.OrderRepository;
using RDP_NTier_Task.DAL.Repostry.ProductRepository;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.PaymentServices
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ICartRepository cartRepo;
        private readonly IConfiguration configurationApp;
        private readonly IOrderRepository orderRepository;
        private readonly IEmailSender emailSender;
        private readonly IProductRepository prodRepo;

        public PaymentServices(ICartRepository cartRepo, IConfiguration configurationApp, IOrderRepository orderRepository, IEmailSender emailSender
           , IProductRepository prodRepo)
        {
            this.cartRepo = cartRepo;
            this.configurationApp = configurationApp;
            this.orderRepository = orderRepository;
            this.emailSender = emailSender;
            this.prodRepo = prodRepo;
        }



        public async Task<PaymentResponse> PaymentProcess(PaymentRequest paymentRequest, string userId, HttpRequest request)
        {
            List<Cart> cartElements = await cartRepo.GetItems(userId);
            if (!cartElements.Any())
            {
                PaymentResponse paymentResponse = new PaymentResponse()
                {
                    success = false,
                    message = "The User Not Have Cart !!!!! "
                };
                return paymentResponse;
            }

            // here create the order : order : after click pay and it is for all pament method : 
            Order order = new Order
            {
                userId = userId,
                shippedDate = DateTime.Now,
                paymentMethod = paymentRequest.PaymentMethod,
                totalPrice = (decimal)cartElements.Sum(ci => ci.product.productPrice * ci.quantity),
            };
            await orderRepository.addOrder(order);

            // if cash or visa : 
            if (paymentRequest.PaymentMethod == PayMethod.cash)
            {
                PaymentResponse paymentResponse = new PaymentResponse()
                {
                    success = false,
                    message = "The User Choose to Pay Cash !!!!! "
                };
                return paymentResponse;
            }

            if (paymentRequest.PaymentMethod == PayMethod.visa)
            {
                StripeConfiguration.ApiKey = configurationApp["Stripe:SecretKey"];
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                    },

                    Mode = "payment",
                    SuccessUrl = $"{request.Scheme}://{request.Host}/api/Customer/PaymentProcesses/Success/{order.OrderId}",
                    CancelUrl = $"{request.Scheme}://{request.Host}/checkout/cancel",
                };

                foreach (var item in cartElements)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.product.productName,
                                Description = item.product.productDescription,
                            },
                            UnitAmount = (long)item.product.productPrice,
                        },
                        Quantity = item.product.quantity,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);

                return new PaymentResponse()
                {
                    success = true,
                    message = "Success To Make Check out !!!!!",
                    URL = session.Url
                };
            }
            return new PaymentResponse()
            {
                success = false,
                message = "Failed To Make Check out !!!!!"
            };


        }

        public async Task<bool> HandlePaymentSuccess(int orderId)
        {
            Order order = await orderRepository.GetUserByOrder(orderId);
            string userId = order.user.Id;

            order.status = orderStatusEnum.completed;

            var cart = await cartRepo.GetItems(userId);
            List<OrderItem> orderItems = new List<OrderItem>();

            // list to update the quantity without n+1 problem . S
            List<(int productId, int quantity)> prodDecrement = new List<(int productId, int quantity)>();

            foreach (var item in cart)
            {
                OrderItem orderr = new OrderItem()
                {
                    orderId = orderId,
                    productId = item.productId,
                    totalPrice = (decimal)item.product.productPrice * item.quantity

                };
                orderItems.Add(orderr);

                // for update quantity : 
                int productId = item.productId;
                int quantity = item.quantity;
                prodDecrement.Add((productId, quantity));

                //// update quantity : 
                //int productQuantity = await prodRepo.decrementQuantity(item.productId, item.quantity);

            }
            // update quantity : 
            int productQuantities = await prodRepo.decrementQuantities(prodDecrement);

            await orderRepository.addOrderItem(orderItems);
            int deleted = await cartRepo.DeleteCartForUser(userId); // to delete cart for this user . 

            // send email : 
            string subject = "";
            string body = "";

            if (order.paymentMethod == PayMethod.visa)
            {
                subject = "Success Pay By Visa ";
                body = "<h1> than you for payment VISA </h1>" +
                    $"<p> you payment for orderId{orderId}</p>" +
                    $"<p> Total Amonut {order.totalPrice}</p>"
                    ;
            }
            else if (order.paymentMethod == PayMethod.cash)
            {
                subject = "Success Pay By Cash ";
                body = "<h1> than you for payment CASH </h1>" +
                    $"<p> you payment for orderId{orderId}</p>" +
                    $"<p> Total Amonut {order.totalPrice}</p>"
                    ;
            }
            await emailSender.SendEmailAsync("adaghara10@gmail.com", subject, body);
            return true;
        }


        //public async Task<int> DeleteCartForUser(string userId)
        //{
        //    int deleted = await cartRepo.DeleteCartForUser(userId);
        //    return deleted;
        //}
    }

}

