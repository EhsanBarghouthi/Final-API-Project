using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RDP_NTier_Task.DAL.DATA;
using RDP_NTier_Task.DAL.DTO.RequestDTO;
using RDP_NTier_Task.DAL.DTO.ResponseDTO;
using RDP_NTier_Task.DAL.Migrations;
using RDP_NTier_Task.DAL.Models;
using RDP_NTier_Task.DAL.Repostry.CartRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDP_NTier_Task.BL.ServicesRepository.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartServices(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
        }
        public async Task<int> AddCart(string userId, int quantity, cartRequest cart)
        {
            Cart newCart = new Cart()
            {
                productId = cart.productId,
                userId = userId,
                quantity = quantity
            };
            return await cartRepository.AddCart(newCart);
        }


        public async Task<CartAllListResponse> GetAll(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new NotImplementedException();

            List<Cart> items = await cartRepository.GetItems(userId);

            List<cartProductResponse> prods = new List<cartProductResponse>();

            foreach (var item in items)
            {
                prods.Add(new cartProductResponse
                {
                    productId = item.productId,
                    productName = item.product.productName,
                    productPrice = (decimal)item.product.productPrice,
                    quantity = item.product.quantity


                });

            }



            CartAllListResponse cartAllListResponse = new CartAllListResponse
            {
                userId = userId,
                cartProducts = prods
            };
            return cartAllListResponse;


        }

        public async Task<int> DeleteElement(string userId, int productId)
        {
            var result = await cartRepository.DeleteItem(userId, productId);
            if (result == 0) return 0;
            return result;
        }

     
    }
}
