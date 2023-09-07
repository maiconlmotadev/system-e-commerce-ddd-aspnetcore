﻿using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    public class UserBuyController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly IUserBuyApp _IUserBuyApp;

        public UserBuyController(UserManager<ApplicationUser> userManager, IUserBuyApp userBuyApp)
        {
            _userManager = userManager;
            _IUserBuyApp = userBuyApp;
        }

        [HttpPost("/api/AddProductCart")]
        public async Task<JsonResult> AddProductCart(string id, string name, string quant)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _IUserBuyApp.Add(new UserBuy
                {
                    IdProduct = Convert.ToInt32(id),
                    BuyQuantity = Convert.ToInt32(quant),
                    State = EnumBuyState.Product_Cart,
                    UserId = user.Id
                });
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}